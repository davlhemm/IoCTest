using System;

namespace IoCTest.Processes
{
    public class BuildAttributeConfigProcess
    {

    }
    
    /// <summary>
    /// Dumb mapping for attribute to configuration writer...
    /// </summary>
    public class AttributeInfo
    {
        /// <summary>
        /// Typically enforced in DWG...
        /// </summary>
        public string BlockName { get; set; }
        /// <summary>
        /// Previously used as presumptive mapper to attribute writer dictionaries...
        /// </summary>
        //TODO: Use something more coherent for attribute information,
        //TODO: Include various block/attribute mappings upfront instead of guessing with magic strings!
        public string Name { get; set; }
        
        public AttributeInfo() { }

        public override string ToString()
        {
            return "Block: " + this.BlockName + ", Name: " + this.Name + "";
        }
    }
}