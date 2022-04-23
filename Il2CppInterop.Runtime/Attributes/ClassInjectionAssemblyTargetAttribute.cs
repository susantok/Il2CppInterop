﻿using System;
using System.Collections.Generic;

namespace Il2CppInterop.Runtime
{
    [Obsolete("Il2CppInterop.Runtime.ClassInjectionAssemblyTargetAttribute is obsolete. Use Il2CppInterop.Runtime.Attributes.ClassInjectionAssemblyTargetAttribute instead.")]
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassInjectionAssemblyTargetAttribute : Il2CppInterop.Runtime.Attributes.ClassInjectionAssemblyTargetAttribute
    {
        public ClassInjectionAssemblyTargetAttribute(string assembly) : base(assembly) { }
        public ClassInjectionAssemblyTargetAttribute(string[] assemblies) : base(assemblies) { }
    }
}

namespace Il2CppInterop.Runtime.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ClassInjectionAssemblyTargetAttribute : Attribute
    {
        string[] assemblies;

        public ClassInjectionAssemblyTargetAttribute(string assembly)
        {
            if (string.IsNullOrWhiteSpace(assembly)) assemblies = new string[0];
            else assemblies = new string[] { assembly };
        }
        public ClassInjectionAssemblyTargetAttribute(string[] assemblies)
        {
            if (assemblies is null) this.assemblies = new string[0];
            else this.assemblies = assemblies;
        }
        internal IntPtr[] GetImagePointers()
        {
            List<IntPtr> result = new List<IntPtr>();
            foreach (string assembly in assemblies)
            {
                IntPtr intPtr = IL2CPP.GetIl2CppImage(assembly);
                if (intPtr != IntPtr.Zero) result.Add(intPtr);
            }
            return result.ToArray();
        }
    }
}