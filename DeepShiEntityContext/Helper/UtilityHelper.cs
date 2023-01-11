using DeepShiEntityModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeepShiEntityContext.Helper
{
    public static class UtilityHelper
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            //string RoleId = Guid.NewGuid().ToString();
            //string UserId = Guid.NewGuid().ToString();

            IdentityRole identityRole = new IdentityRole { Name = "System", NormalizedName = "System".ToUpper() };
            ApplicationUser applicationUser = new ApplicationUser
            {
                Department = "System",
                Email = "superadmin@DeepShi.in",
                EmployeeId = "0",
                UserName = "superadmin@DeepShi.in",
                NormalizedUserName = "superadmin@DeepShi.in".ToUpper(),
                NormalizedEmail = "superadmin@DeepShi.in".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Superadmin@2022#"),
                EmpDesignation = "System",
                EmployeeName = "DeepShi System",
                EmpDivision = "System",
                EmailConfirmed = true

            };

            modelBuilder.Entity<IdentityRole>().HasData(identityRole);
            modelBuilder.Entity<ApplicationUser>().HasData(
               applicationUser
                );
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = identityRole.Id,
                UserId = applicationUser.Id
            });


        }

        public static DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            Props = Props.Where(x => !x.PropertyType.Name.ToLower().Contains("list") && !x.PropertyType.Name.ToLower().Contains("collection")).ToArray();
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                //dataTable.Columns.Add(prop.Name);
                if (!prop.PropertyType.Name.ToLower().Contains("list") && !prop.PropertyType.Name.ToLower().Contains("collection"))
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    if (!Props[i].PropertyType.Name.ToLower().Contains("list") && !Props[i].PropertyType.Name.ToLower().Contains("collection"))
                    {
                        values[i] = Props[i].GetValue(item, null);
                    }
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
    //public static class SqlProcedures
    //{
    //    public const string SPAddClients = "sp_insert_update_client";
    //    public const string SPPosts = "sp_insert_update_post";
    //    public const string SPCRCodes = "sp_cr_code";
    //    public const string SPCadres = "sp_cadre_code";
    //}
}
