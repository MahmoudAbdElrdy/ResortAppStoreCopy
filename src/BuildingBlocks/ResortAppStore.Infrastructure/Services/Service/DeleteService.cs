using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.Service
{
    public class DeleteService
    {
        private readonly IDapperRepository<ScriptDeleteDto> _query;
        public DeleteService(IDapperRepository<ScriptDeleteDto> queryDb)
        {

            _query = queryDb;
        }
        public async Task<bool> ScriptCheckDelete(string tableName, string columnName, object value)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);

            foreach (var item in result.ToList())
            {
                var q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}={value} and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
        }
        public async Task<bool> ScriptCheckDeleteWithDetails(string tableName, string columnName, object value,List<string> Details)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);

            foreach (var item in result.ToList())
            {
                if (Details.Contains(item.RelatedTable))
                {
                    break;
                }
                var q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}={value} and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
        }

        public async Task<bool> ScriptCheckDelete(string tableName, string columnName, long value)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);

            foreach (var item in result.ToList())
            {
                var q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}={value} and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
        }
        public async Task<bool> ScriptCheckDelete(string tableName, string columnName, string value)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);

            foreach (var item in result.ToList())
            {
                var q = "";

                q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}='{value}' and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
        }
        public async Task<bool> ScriptCheckDelete(string tableName, string columnName, Guid value)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);

            foreach (var item in result.ToList())
            {
                var q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}='{value}' and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
        }
        public async Task<bool> ScriptCheckDeleteExcluded(string tableName, string columnName, string value, List<string> excluded)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);
            result = result.Where(c => !excluded.Contains(c.RelatedTable));
            foreach (var item in result.ToList())
            {
                var q = "";

                q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}='{value}' and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
            //  result = result.Where(c => !excluded.Contains(c.RelatedTable));
        }
        public async Task<bool> ScriptCheckDeleteExcluded(string tableName, string columnName, int value, List<string> excluded)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);
            result = result.Where(c => !excluded.Contains(c.RelatedTable));
            foreach (var item in result.ToList())
            {
                var q = "";

                q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}={value} and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
            //  result = result.Where(c => !excluded.Contains(c.RelatedTable));
        }
        public async Task<bool> ScriptCheckDeleteExcluded(string tableName, string columnName, Guid value, List<string> excluded)
        {

            var query = $@"
                   SELECT cu.TABLE_NAME AS RelatedTable, cu.COLUMN_NAME AS RelatedColumn
                     FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS c
                      INNER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE cu
                         ON cu.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                        INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE ku
                         ON ku.CONSTRAINT_NAME = c.UNIQUE_CONSTRAINT_NAME
                      WHERE ku.TABLE_NAME ='{tableName}'  and Ku.COLUMN_NAME = '{columnName}'";
            var result = await _query.QueryAsync<ScriptDeleteDto>(query);
            result = result.Where(c => !excluded.Contains(c.RelatedTable));
            foreach (var item in result.ToList())
            {
                var q = "";

                q = $@"select * from {item.RelatedTable} where {item.RelatedColumn}='{value}' and IsDeleted=0";

                var checkIsdelete = _query.Querydynamic(q);

                if (checkIsdelete.Count() > 0)
                {
                    return false;
                }

            }

            return true;
            //  result = result.Where(c => !excluded.Contains(c.RelatedTable));
        }



    }
    public class ScriptDeleteDto
    {
        public string RelatedTable { get; set; }
        public string RelatedColumn { get; set; }
    }
}
