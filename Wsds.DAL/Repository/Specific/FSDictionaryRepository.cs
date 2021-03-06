﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Wsds.DAL.Entities;
using Wsds.DAL.Providers;
using Wsds.DAL.Repository.Abstract;
using FoxStoreDBContext = Wsds.DAL.ORM.FoxStoreDBContext;


namespace Wsds.DAL.Repository.Specific
{
    public class FSDictionaryRepository : IDictionaryRepository
    {
        private FoxStoreDBContext _context;
        private ICacheService<Product_DTO> _csp;
        private ICacheService<Currency_DTO> _cur;
        private ICacheService<Manufacturer_DTO> _mnf;
        private ICacheService<Quotation_Product_DTO> _qtp;
        private ICacheService<Supplier_DTO> _supl;
        //private ICacheService _csg;

        public FSDictionaryRepository(FoxStoreDBContext dbContext,
                                      ICacheService<Product_DTO> csp,
                                      ICacheService<Currency_DTO> csc,
                                      ICacheService<Manufacturer_DTO> mnf,
                                      ICacheService<Quotation_Product_DTO> qtp,
                                      ICacheService<Supplier_DTO> supl/*,
                                      ICacheService<Product_Group> csg*/) {
            _context = dbContext;
            _csp = csp;
            _cur = csc;
            _mnf = mnf;
            _qtp = qtp;
            _supl = supl;
            /*
            _csg = csg;
            */
        }

        public IEnumerable<Manufacturer_DTO> Manufacturers => _mnf.Items.Values.ToList();
        public Manufacturer_DTO Manufacturer(long id) => _mnf.Item(id);


        public IEnumerable<Currency_DTO> Currencies => _cur.Items.Values.ToList();
        public Currency_DTO Currency(long id) => _cur.Item(id);

        public IEnumerable<Quotation_Product_DTO> QuotationProducts => _qtp.Items.Values.ToList();
        public Quotation_Product_DTO QuotationProduct(long id) => _qtp.Item(id);

        public IEnumerable<Supplier_DTO> Suppliers => _supl.Items.Values.ToList();
        public Supplier_DTO Supplier(long id) => _supl.Item(id);


        public IEnumerable<Currency> CurAscending => _context.CURRENCIES.OrderBy(c => c.SHORT_NAME);

        public IEnumerable<Product_Group> ProductGroupsByFilterCache(Func<Product_Group, bool> filter)
        {
            return null;//_csg.Items.Values.Where(filter);
        }

        public Product_Group ProductGroupSingleByFilterCache(Func<Product_Group, bool> singleFilter)
        {
            return null; // _csg.Items.Values.FirstOrDefault(singleFilter);
        }

        public IEnumerable<Product_Group> ProductGroups => _context.PRODUCT_GROUPS;
        public Product_DTO Product(long id) => _csp.Item(id);

        
        public IEnumerable<Product_DTO> Products => _csp.Items.Values.ToList();

        public IEnumerable<Product_Group> ProductsGroupsCache => null;//_csg.Items.Values;

    }
}