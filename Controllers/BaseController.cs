using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using HordeFlow.HR.Repositories.Interfaces;
using HordeFlow.HR.Infrastructure.Models;

namespace HordeFlow.Hris.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController<T> : Controller where T : class, IBaseEntity, new()
    {
        protected IRepository<T> repository;

        public BaseController(IRepository<T> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await repository.List());
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Search(int? currentPage = 1, int? pageSize = 100, string filter = "", string sort = "", string fields = "")
        {
            try
            {
                var entities = await repository.Search(currentPage, pageSize, filter, sort, fields);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await repository.Get(id);
            if (entity != null)
                return Ok(entity);
            return NotFound(entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] T entity)
        {
            if (entity == null || entity.Id != id)
            {
                return BadRequest();
            }

            var persistedEntity = await repository.Get(id);
            if(persistedEntity == null)
            {
                return NotFound();   
            }

            //repository.Update(entity);
            await repository.Commit();
            
            return new NoContentResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]T entity)
        {
            if(entity == null)
                return BadRequest();

            await repository.Insert(entity);
            await repository.Commit();
            return CreatedAtAction("Get", new { id = entity.Id }, entity); // This will return the location of the inserted entity.
            //return Ok(entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var persistedEntity = await repository.Get(id);
            if(persistedEntity == null)
            {
                return NotFound();   
            }

            repository.Delete(persistedEntity);
            await repository.Commit();
            return new NoContentResult();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Sync([FromBody]BulkEntity<T> entities)
        {
            try
            {
                foreach (T e in entities.Added)
                {
                    if (this.ModelState.IsValid)
                        await repository.Insert(e);
                }

                foreach (T e in entities.Edited)
                {
                    if (this.ModelState.IsValid)
                        repository.Update(e);
                }

                foreach (T e in entities.Removed)
                {
                    if (this.ModelState.IsValid)
                        repository.DeleteWhere(p => p.Id == e.Id);
                }
                await repository.Commit();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return Ok(entities);
        }
    }
}