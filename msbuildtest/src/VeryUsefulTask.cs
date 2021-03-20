
using System.ComponentModel.DataAnnotations;

namespace msbuildtest
{
    public class VeryUsefulTask : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Name { get; set; }
        
        public override bool Execute()
        {
            Log.LogMessage($"You Provide for Property Name: {Name}");
            return true;
        }
    }
}