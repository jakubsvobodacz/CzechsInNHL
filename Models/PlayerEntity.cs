using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CzechsInNHL.Models
{
    public class PlayerEntity : TableEntity
    {
        public string PlayerID { get; set; }
        public string FullName { get; set; }

        public void AssignRowKey()
        {
            this.PartitionKey = PlayerID;
        }
        public void AssignPartitionKey()
        {
            this.RowKey = FullName;
        }
    }
}
