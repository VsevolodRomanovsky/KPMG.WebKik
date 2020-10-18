using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace KPMG.WebKik.Web
{
    public static class UnityExtensions
    {
        public static IUnityContainer RegisterContextType<TFrom, TTo>(this IUnityContainer container, LifetimeManager manager = null, params InjectionMember[] injections) where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(manager ?? new HierarchicalLifetimeManager(), injections);
        }

        public static IUnityContainer RegisterContextTypeNamed<TFrom, TTo>(this IUnityContainer container, string name, LifetimeManager manager = null, params InjectionMember[] injections) where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(name, manager ?? new HierarchicalLifetimeManager(), injections);
        }

        public static IUnityContainer RegisterContextType(this IUnityContainer container, Type from, Type to, LifetimeManager manager = null, params InjectionMember[] injections)
        {
            return container.RegisterType(from, to, manager ?? new HierarchicalLifetimeManager(), injections);
        }

        public static IUnityContainer RegisterContextType<T>(this IUnityContainer container, LifetimeManager manager = null, params InjectionMember[] injections)
        {
            return container.RegisterType<T>(manager ?? new HierarchicalLifetimeManager(), injections);
        }

        public static IUnityContainer RegisterTracingType<TFrom, TTo>(this IUnityContainer container, LifetimeManager manager = null, params InjectionMember[] injections) where TTo : TFrom
        {
            container.RegisterContextType<TFrom, TTo>(manager, injections);
            ConfigureInterception<TFrom>(container);
            return container;
        }

        public static IUnityContainer RegisterTracingTypeNamed<TFrom, TTo>(this IUnityContainer container, string name, LifetimeManager manager = null, params InjectionMember[] injections) where TTo : TFrom
        {
            container.RegisterContextTypeNamed<TFrom, TTo>(name, manager, injections);
            ConfigureInterception<TFrom>(container);
            return container;
        }

        public static IUnityContainer RegisterTracingTypeNamed(this IUnityContainer container, Type from, Type to, string name, LifetimeManager manager = null, params InjectionMember[] injections)
        {
            container.RegisterType(from, to, name, manager ?? new HierarchicalLifetimeManager(), injections);
            ConfigureInterception(from, container);
            return container;
        }

        public static IUnityContainer RegisterTracingType(this IUnityContainer container, Type from, Type to, LifetimeManager manager = null, params InjectionMember[] injections)
        {
            container.RegisterContextType(from, to, manager, injections);
            ConfigureInterception(from, container);
            return container;
        }

        public static IUnityContainer RegisterTracingType<T>(this IUnityContainer container, LifetimeManager manager = null, params InjectionMember[] injections)
        {
            container.RegisterContextType<T>(manager, injections);
            ConfigureInterception<T>(container);
            return container;
        }

        private static void ConfigureInterception<T>(IUnityContainer container)
        {
            var interception = container.Configure<Interception>();
            if (typeof(T).IsInterface)
                interception.SetDefaultInterceptorFor<T>(new InterfaceInterceptor());
            else
                interception.SetDefaultInterceptorFor<T>(new VirtualMethodInterceptor());
        }

        private static void ConfigureInterception(Type t, IUnityContainer container)
        {
            var interception = container.Configure<Interception>();
            if (t.IsInterface)
                interception.SetDefaultInterceptorFor(t, new InterfaceInterceptor());
            else
                interception.SetDefaultInterceptorFor(t, new VirtualMethodInterceptor());
        }
    }

}
