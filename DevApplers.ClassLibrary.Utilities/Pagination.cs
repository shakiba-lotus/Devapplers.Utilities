using System;
using System.Collections.Generic;
using System.Linq;

namespace DevApplers.ClassLibrary.Utilities
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public Pager Pager { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            //calculate total, start and end pages
            var totalPages = (int) Math.Ceiling(totalItems / (decimal) pageSize);
            var currentPage = page ?? 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int StartPage { get; }
        public int EndPage { get; }
    }

    public static class Pagination
    {
        public static Pagination<T> ToPager<T, TKeys>(this IEnumerable<T> items, Func<T, TKeys> orderByExpr,
            int? page = 1, int pageSize = 10, bool isOrderByAscending = true)
        {
            if (page == null)
                page = 1;

            var enumerable = items as T[] ?? items.ToArray();
            var pager = new Pager(enumerable.Count(), page.Value, pageSize);
            var paginator = new Pagination<T>
            {
                Pager = pager
            };
            if (isOrderByAscending)
            {
                paginator.Items = enumerable.AsEnumerable()
                    .OrderBy(orderByExpr)
                    .Skip((page.Value - 1) * pager.PageSize)
                    .Take(pager.PageSize).ToList();
            }
            else
            {
                paginator.Items = enumerable.AsEnumerable()
                    .OrderByDescending(orderByExpr)
                    .Skip((page.Value - 1) * pager.PageSize)
                    .Take(pager.PageSize).ToList();
            }

            return paginator;
        }

        public static Pagination<T> ToPager<T, TKeys, TKeys1>(this IEnumerable<T> items, Func<T, TKeys> orderByExpr,
            int? page = 1, int pageSize = 10, Func<T, TKeys1> thenByExpr = null, bool isOrderByAscending = true,
            bool isThenByAscending = true)
        {
            if (page == null)
                page = 1;

            var enumerable = items as T[] ?? items.ToArray();
            var pager = new Pager(enumerable.Count(), page.Value, pageSize);
            var paginator = new Pagination<T>
            {
                Pager = pager
            };
            if (isOrderByAscending)
            {
                paginator.Items = enumerable.AsEnumerable()
                    .OrderBy(orderByExpr)
                    .Skip((page.Value - 1) * pager.PageSize)
                    .Take(pager.PageSize).ToList();

                if (thenByExpr == null) return paginator;
                if (isThenByAscending)
                {
                    paginator.Items = enumerable.AsEnumerable()
                        .OrderBy(orderByExpr)
                        .ThenBy(thenByExpr)
                        .Skip((page.Value - 1) * pager.PageSize)
                        .Take(pager.PageSize).ToList();
                }
                else
                {
                    paginator.Items = enumerable.AsEnumerable()
                        .OrderBy(orderByExpr)
                        .ThenByDescending(thenByExpr)
                        .Skip((page.Value - 1) * pager.PageSize)
                        .Take(pager.PageSize).ToList();
                }
            }
            else
            {
                paginator.Items = enumerable.AsEnumerable()
                    .OrderByDescending(orderByExpr)
                    .Skip((page.Value - 1) * pager.PageSize)
                    .Take(pager.PageSize).ToList();

                if (thenByExpr == null) return paginator;
                if (isThenByAscending)
                {
                    paginator.Items = enumerable.AsEnumerable()
                        .OrderByDescending(orderByExpr)
                        .ThenBy(thenByExpr)
                        .Skip((page.Value - 1) * pager.PageSize)
                        .Take(pager.PageSize).ToList();
                }
                else
                {
                    paginator.Items = enumerable.AsEnumerable()
                        .OrderByDescending(orderByExpr)
                        .ThenByDescending(thenByExpr)
                        .Skip((page.Value - 1) * pager.PageSize)
                        .Take(pager.PageSize).ToList();
                }
            }

            return paginator;
        }
    }
}