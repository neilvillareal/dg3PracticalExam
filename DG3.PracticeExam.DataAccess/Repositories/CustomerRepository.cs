using DG3.PracticeExam.Domain.Entities;
using DG3.PracticeExam.Domain.Interfaces;
using System.Data;

namespace DG3.PracticeExam.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, IRepository<Customer>
    {
        private const string PARAM_CUSTOMER_ID = "@customer_id";
        private const string PARAM_FIRST_NAME = "@first_name";
        private const string PARAM_LAST_NAME = "@last_name";
        private const string PARAM_ADDRESS_1 = "@address_1";
        private const string PARAM_ADDRESS_2 = "@address_2";
        private const string PARAM_CITY = "@city";
        private const string PARAM_PROVINCE = "@province";
        private const string PARAM_POSTCODE = "@postcode";
        private const string PARAM_COUNTRY = "@country";
        private const string PARAM_EMAIL = "@email";
        private const string PARAM_REGISTRATION_DATE = "@registration_date";

        private readonly IDbManager manager;

        public CustomerRepository(IDbManager dbManager)
        {
            this.manager = dbManager;
        }


        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool InserBulk(IEnumerable<Customer> entities)
        {
            throw new NotImplementedException();
        }

        public int Insert(Customer entity)
        {
            int ret = 0;

            try
            {
                manager.AssignStoredProc("customer_insert");
                manager.SetInputParams(PARAM_FIRST_NAME, DbType.String, entity.Firstname);
                manager.SetInputParams(PARAM_LAST_NAME, DbType.String, entity.Lastname);
                manager.SetInputParams(PARAM_ADDRESS_1, DbType.String, entity.Address1);
                manager.SetInputParams(PARAM_ADDRESS_2, DbType.String, entity.Address2);
                manager.SetInputParams(PARAM_CITY, DbType.String, entity.City);
                manager.SetInputParams(PARAM_PROVINCE, DbType.String, entity.Province);
                manager.SetInputParams(PARAM_POSTCODE, DbType.String, entity.Postcode);
                manager.SetInputParams(PARAM_COUNTRY, DbType.String, entity.Country);
                manager.SetInputParams(PARAM_EMAIL, DbType.String, entity.Email);
                manager.SetInputParams(PARAM_REGISTRATION_DATE, DbType.DateTime, entity.Regdate);

                manager.SetOutputParams(PARAM_CUSTOMER_ID, DbType.Int32);

                if (manager.RunNonQuery())
                   int.TryParse(manager.GetOutputParams(PARAM_CUSTOMER_ID)?.ToString(), out ret);
            }
            catch (Exception e)
            {
                ret = 0;
            }

            return ret;
        }

        public bool Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Customer SetData(DataRow dr)
        {
            Customer customer = new Customer();


            return customer;
        }
    }
}
