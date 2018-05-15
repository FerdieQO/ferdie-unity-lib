using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FerdieUnityLib.Attributes
{
    /// <summary>
    /// Attribute to enable displaying the read-friendly name of an enumerator
    /// For example: [EnumFlag("Type of Farming Wheel Item")]
    /// </summary>
    public class EnumFlagAttribute : PropertyAttribute
    {
        /// <summary>
        /// The enum name
        /// </summary>
        public string EnumName;
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumFlagAttribute"/> class.
        /// </summary>
        public EnumFlagAttribute() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumFlagAttribute"/> class.
        /// </summary>
        /// <param name="name">The enum name</param>
        public EnumFlagAttribute(string name)
        {
            EnumName = name;
        }
    }
}
