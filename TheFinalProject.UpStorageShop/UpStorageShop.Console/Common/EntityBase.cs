using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpStorageShop.Console.Common
{
    public class EntityBase<T>
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; }
        public string ModifiedByUserId { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string DeletedByUserId { get; set; }
        public DateTimeOffset DeletedOn { get; set; }
    }
}
