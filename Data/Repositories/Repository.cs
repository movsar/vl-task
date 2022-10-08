using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories {
    public class Repository<TModel> where TModel : DbModelBase {
        private readonly WarehouseContext _context;
        public Repository(WarehouseContext context) {
            _context = context;
        }
     
    }

}
