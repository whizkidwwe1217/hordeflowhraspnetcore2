using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HordeFlow.HR.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyModel;

namespace HordeFlow.HR.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        private static IEnumerable<Type> mappingTypes;
        private static IEnumerable<Type> entityTypes;

        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly, Type mappingInterface)
        {
            if (mappingTypes == null)
                mappingTypes = assembly.GetTypes().Where(x => !x.IsAbstract && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
            return mappingTypes;
        }


        public static void AddEntityConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetMappingTypes(typeof(IEntityMappingConfiguration<>));
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityMappingConfiguration>())
            {
                config.Map(modelBuilder);
            }
        }

        public static void AddEntityCompanyFilter(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var mappingTypes = assembly.GetEntityTypes(typeof(IEntityCompanyFilter<>));
            foreach (var config in mappingTypes.Select(Activator.CreateInstance).Cast<IEntityCompanyFilter>())
            {
                config.Filter(modelBuilder);
            }
        }
        
        private static IEnumerable<Type> GetEntityTypes(this Assembly assembly, Type entityInterface)
        {
            if (entityTypes == null)
                entityTypes = (from a in GetReferencingAssemblies()
                        from t in a.DefinedTypes
                        where t.BaseType == typeof(IEntityCompanyFilter<>)
                        select t.AsType());
            return entityTypes;
        }

        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }
    }
}