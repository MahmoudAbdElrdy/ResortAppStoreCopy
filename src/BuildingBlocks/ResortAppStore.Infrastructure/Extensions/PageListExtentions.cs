﻿
using Common.Infrastructures;
using Common.Interfaces;
using X.PagedList;

namespace Common.Extensions
{
    public static class PageListExtensions
    {
        public static async Task<PageList<T>> ToPagedListAsync<T>(this IQueryable<T> query, IPaging command, CancellationToken cancellationToken)
        {
            var (page, pageSize) = (1, 10);

            page = command.PageIndex > 0 ? command.PageIndex : page;
            pageSize = command.PageSize > 0 ? command.PageSize : pageSize;
            var result = await query.ToPagedListAsync(page, pageSize,10000, cancellationToken);

            return new PageList<T>(result);
        }
    }
}