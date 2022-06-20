using System;
using Microsoft.EntityFrameworkCore;

namespace Amzaon_DataWarehouse_BackEnd.Services
{
    public class PaginatedListService<T> : List<T>
    {
        public int PageIndex { get; private set; }//当前页
        public int TotalPages { get; private set; }//总页数

        public PaginatedListService(List<T> items, int count, int pageIndex, int pageSize)//count：总条数 ，pageIndex：当前页，pageSize：每页显示行数
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }
        //判断是否为首页
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        //判断是否为尾页
        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        public static async Task<PaginatedListService<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedListService<T>(items, count, pageIndex, pageSize);
        }
    }
}

