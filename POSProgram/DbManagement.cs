using Dapper;
using MySql.Data.MySqlClient;
using POSProgram.Entry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace POSProgram
{
    class DbManagement
    {
        private Conn conn = new Conn();
        private static string usersInfo;
        private static string statusInfo;
        private static string emID;
        private bool checkLogin = false;
        private static string proIDandName;
        private static string quantity;
       

        public string Quantity { get => quantity; set => quantity = value; }

        //var database 
        //  private SqlConnection connection;

        public DbManagement() { }

        //check connect to db
        public void checkConnect()
        {
            using (var connection = new SqlConnection(conn.getConnection()))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                    connection.Close();
                    MessageBox.Show("connection Successful...");
                }
            }
        }

        //Method login User and Admin
        public void Login(string user , string password)
        {
            try
            {
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    string sql = "select * from Login where UserName=@user and Password=@pass";
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@user", user.Trim());
                        cmd.Parameters.AddWithValue("@pass", password.Trim());
                        ///
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            //Login

                            emID = dt.Rows[0][1].ToString();
                            usersInfo = dt.Rows[0][2].ToString();
                            statusInfo = dt.Rows[0][6].ToString();
                            connection.Close();
                            checkLogin = true;
                           // MessageBox.Show("Login Successful..."+status,users);
                            LoadingPage loading = new LoadingPage(LoadingScreen);
                            loading.ShowDialog();
                            var main = new MainWindow(statusInfo , usersInfo,emID);
                            main.Show();


                        }
                        else
                        {
                            //Error
                            MessageBox.Show("Login Error...");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.GetType().Name + "Error");
            }
        }
        // return status in account login
        public string StatusUser()
        {
            return statusInfo;
        }
        // return username in account login
        public string UserName()
        {
            return usersInfo;
        }
        public bool CheckLogin()
        {
            return checkLogin;
        }
        public string EmId()
        {
            return emID;
        }
        private void LoadingScreen()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(5);
            }
        }
       

        //manager Brand
        public void insertBrand(string id,string brandId , string brandname)
        {
            try
            {
                string sql = "insert into Brand (brandId ,brandName) values(@brandId,@brandName)";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    BrandEntry b = new BrandEntry(Convert.ToInt16(null), brandId , brandname);
                   
                    var resulte = connection.Execute(sql , b);
                    MessageBox.Show("SAVE Successful");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // update Brand Method
        public void UpdateBrand(string  ID, string brandId, string brandname)
        {
            try
            {
                string sql = "update Brand set brandId=@brandId, brandName=@brandName where ID=@Id ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    BrandEntry brand = new BrandEntry(Convert.ToInt16(ID),brandId, brandname);

                    var resulte = connection.Execute(sql, brand);
                    MessageBox.Show("Update Successful");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //delete Brand
        public void RemoveBrand(string ID)
        {
            try
            {
                string sql = "delete from Brand  where ID='" + Convert.ToInt16(ID) + "'";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                   // BrandEntry brand = new BrandEntry(brandId, brandname);

                    var resulte = connection.Execute(sql);
                    MessageBox.Show("Remove Successful");
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Brand show data
        public IEnumerable<BrandEntry> BrandData()
        {
            IEnumerable<BrandEntry> result = null;
            try
            {
                string sql = "select * from Brand ";
                using (var  con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();

                     result = con.Query<BrandEntry>(sql);
                    con.Close();

                    return result;
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           return result;
        }
        //Manager Category
        public void insertCategory(string Id,string cateId,string cateName , string descrip)
        {
            try
            {
                string sql = "insert into Categorys (categoryId , categoryName , Description) values (@categoryId ,@categoryName,@Description) ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    CategoryEntry category = new CategoryEntry(Convert.ToInt16(null), cateId ,cateName , descrip);
                    connection.Execute(sql, category);

                    connection.Close();
                    MessageBox.Show("Save successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        public void UpdateCategory(string id ,string cateId, string cateName, string descrip)
        {
            try
            {
                string sql = "update Categorys set categoryId=@categoryId , categoryName = @categoryName, Description=@Description where ID=@Id ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    CategoryEntry category = new CategoryEntry(Convert.ToInt16(id),cateId, cateName, descrip);
                    connection.Execute(sql, category);

                    connection.Close();
                    MessageBox.Show("Update successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //delete category 
        public void RemoveCategory(string Id)
        {
            try
            {
                string sql = "delete from Categorys where ID='"+Convert.ToInt16(Id)+"' ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                   
                    connection.Execute(sql);

                    connection.Close();
                    MessageBox.Show("Remove successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // get data category to gridview
        public IEnumerable<CategoryEntry> getDataCategory()
        {
            try
            {
                string sql = "select * from Categorys ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    var result = connection.Query<CategoryEntry>(sql);
                    connection.Close();
                    return result;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        //Product manager
        public void insertProduct(string Id ,string proId, string proName , string supplierID , string cateId, string brandId, string unit , string price)
        {
            try
            {
                string sql = "insert into Products  values(@productId,@productName,@supplierId,@categoryId,@brandId,@Unit,@price)";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    ProductsEntry products = new ProductsEntry(Convert.ToInt16(null), proId, proName, supplierID, cateId,brandId, unit, price);

                    connection.Execute(sql,products);

                    connection.Close();
                    MessageBox.Show("Save successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //update products
        public void UpdateProduct(string Id,string proId, string proName, string supplierID, string cateId, string brandId, string unit, string price)
        {
            try
            {
                string sql = "update products set productId=@productId,productName= @productName ,supplierId=@supplierId ,categoryId=@categoryId,  brandId=@brandId ,Unit=@Unit,price=@price where ID=@Id ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    ProductsEntry products = new ProductsEntry(Convert.ToInt16(Id),proId, proName, supplierID, cateId,brandId, unit, price);

                    connection.Execute(sql, products);

                    connection.Close();
                    MessageBox.Show("Update successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //delete product
        public void removeProduct(string id)
        {
            try
            {
                string sql = "delete from Products   where ID='"+Convert.ToInt16(id)+"'";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    //ProductsEntry products = new ProductsEntry(proId, "", "", "", "", "");

                    connection.Execute(sql);

                    connection.Close();
                    MessageBox.Show("Remove successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        // get product db show to table 
        public DataTable getDataProducts()
        {
            var table = new DataTable();
            try
            {
                string sql = "select * from Productjoin";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        // search product db show to table in cart get Item
        public DataTable searchProductsCart(string pId)
        {
            var table = new DataTable();
            try
            {
                string sql = "select * from SearchProduct where ProductID='" + pId+"'";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        // get product db show to table in cart get Item
        public DataTable getProductsCart()
        {
            var table = new DataTable();
            try
            {
                string sql = "select * from SearchProduct ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        public void setTextSearchProduct(string proID)
        {
            proIDandName = proID;
        }
        public bool CheckTextSeach()
        {
            if (string.IsNullOrEmpty(proIDandName))
            {
                return false;
            }
           // MessageBox.Show(proIDandName + "");
            return true;
        }
        // get product db show to table 
        public DataTable SearchProducts()
        {
            var table = new DataTable();
            try
            {
                string sql = "select * from Productjoin where ProductID=@id or ProductName=@pname or CategoryName=@cateName or BrandName=@bName ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        // add parameter to cmd 
                        cmd.Parameters.AddWithValue("@id", proIDandName);
                        cmd.Parameters.AddWithValue("@pname", proIDandName);
                        cmd.Parameters.AddWithValue("@cateName", proIDandName);
                        cmd.Parameters.AddWithValue("@bName", proIDandName);
                        // add cmd to adapter
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        proIDandName = "";
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        //Brand Id to comboBox
        public DataTable getItemBrandName()
        {
           
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from Brand";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql,connection))
                    {
                       
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                        //   LenghtData;
                       // MessageBox.Show(dataTable.Rows.Count+"");


                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           return null;
        }
        // get values from combobox brand and take search in table brand get brand ID
        public string getItemBrandId(string brandName)
        {
            string BrandId = "";
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  brandId from Brand where brandName='"+brandName+"'";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        //   LenghtData;
                        // MessageBox.Show(dataTable.Rows.Count+"");
                        BrandId = dataTable.Rows[0][0].ToString();
                        connection.Close();
                       // MessageBox.Show(BrandId);

                    }
                }
                return BrandId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        //get NAme Category
        public DataTable getItemCategoryName()
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from Categorys";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                       // MessageBox.Show(dataTable.Rows[0][2] + "");
                        return dataTable;
                        //   LenghtData;
              
                    }
                }
               // return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        // search send parameter cate name  return cateId
        public string getItemCategoryId(string cateName)
        {
            string CateID = "";
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  categoryId from Categorys where categoryName='" + cateName+ "'";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        //   LenghtData;
                        // MessageBox.Show(dataTable.Rows.Count+"");
                        CateID = dataTable.Rows[0][0].ToString();
                        connection.Close();
                         //MessageBox.Show(CateID);

                    }
                }
                return CateID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        //Login management
        //selecte data from table login
        public DataTable getDataLogin()
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select * from Login";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        // get data to gridview
        public DataTable getDataLoginEntry()
        {
            var table = new DataTable();
            try
            {
                string sql = "select * from Login";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        connection.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        // get data search photo to icon user
        public string getIconUser()
        {
            var table = new DataTable();
            try
            {
                string sql = "select Photo from Login where UserName='"+UserName()+"'";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();
                    using (var cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                       // var image = table.Rows[0][0].ToString();
                       // MessageBox.Show(StatusUser());
                        connection.Close();

                        return table.Rows[0][0].ToString(); ;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"Not Your Image");
            }
            return null;
        }
        // save data to db login 
        public void InsertLogin(string employeeId,string userName , string password , string birthDate , string photo , string note)
        {
            try
            {
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var sql = "insert into Login (EmployeeId , UserName , Password , BirthDate , photo,Note) values(@employeeId , @userName , @password,@birthDate , @photo , @note)";
                    var parameter = new LoginEnty(null,employeeId,userName,password,birthDate,photo,note);
                    var resulte = con.Execute(sql , parameter);
                    con.Close();
                    MessageBox.Show("Save successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //update table
        public void UpdateLogin(string Id,string employeeId, string userName, string birthDate, string photo, string note)
        {
            try
            {
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var sql = "update Login set EmployeeId =@employeeId , UserName= @userName , BirthDate=@birthDate , photo= @photo ,Note= @note where ID=@Id";
                    var parameter = new LoginEnty(Id, employeeId, userName,null, birthDate, photo, note);
                    var resulte = con.Execute(sql, parameter);
                    con.Close();
                    MessageBox.Show("Update successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //remove element table login
        public void RemoveLogin(string id)
        {
            try
            {
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var sql = "delete from Login where ID='"+Convert.ToInt32(id)+"'";
                    var resulte = con.Execute(sql);
                    con.Close();
                    MessageBox.Show("Remove successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //remove Password
        public void UpdatePasswordLogin(string id , string password)
        {
            try
            {
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var sql = "update Login set Password='"+password+"' where ID='" + Convert.ToInt32(id) + "'";
                    var resulte = con.Execute(sql);
                    con.Close();
                    MessageBox.Show("successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        // stock manager
        // insert stock method
        public void InsertStock(string stockId , string cateId, string proId, string unit , string date ,string quantity , string employee , string bal)
        {
            try
            {
                var sql = "insert into Stock values(@stockId , @categoryId ,@productId ,@unit , @stockDate , @Quantity ,@employeeId , @bal)";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    StockEntity stock = new StockEntity(stockId,cateId,proId,unit,date,quantity,employee ,bal);
                    var result = con.Execute(sql ,stock);
                    con.Close();
                   // MessageBox.Show("Save Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //update stock method
        public void UpdateStock(string stockId, string cateId, string proId, string unit, string date, string quantity, string employee, string bal)
        {
            try
            {
                var sql = "update Stock set categoryId=@categoryId , productId=@productId ,unit=@unit , stockDate=@stockDate , Quantity=@quantity ,EmployeeId=@employeeId , bal=@bal where stockID=@stockId";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    StockEntity stock = new StockEntity(stockId, cateId, proId, unit, date, quantity, employee, bal);
                    var result = con.Execute(sql, stock);
                    con.Close();
                    MessageBox.Show("Update Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //remove stock method
        public void RemovStock(string stockId)
        {
            try
            {
                var sql = "delete from Stock where stockID='"+stockId+"'";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                   // StockEntity stock = new StockEntity(stockId, cateId, proId, unit, date, quantity, employee, bal);
                    var result = con.Execute(sql);
                    con.Close();
                    MessageBox.Show("Remove Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        // update stock quantity for products
        public void UpdateQuantStock(string proID , int quant)
        {
            try
            {
                var sql = "update Stock set bal=(bal-'"+quant+"')  where productId='"+proID+"' ";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    var cmd = new SqlCommand(sql,con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Update Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        // get items from stock into datagridview
        public DataTable GetDataStock()
        {
            var table = new DataTable();
            try
            {
                // join table stock and category , employee , products
                var sql = "select * from Stockjoin";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        con.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Get data Error");
            }
            return null;
        }

        //get numner stock quantity
        public int numnerQutStock(string proID)
        {
            var table = new DataTable();
            try
            {
                // join table stock and category , employee , products
                var sql = "select * from stockQuantity  where productId='"+proID+"' ";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        con.Close();
                        return Convert.ToInt32(table.Rows[0][1].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Get data Error");
            }
            return 0;
        }

        //get NAme product
        public DataTable getItemproductName()
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from products";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                        return dataTable;
                        //   LenghtData;
                        // MessageBox.Show(dataTable.Rows.Count+"");


                    }
                }
                // return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        //get NAme product
        public DataTable getSearchProductName(string proID)
        {
            try
            {
                //MessageBox.Show(pId);
                string sql = "select  * from Products where ProductID='" +proID+"'";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        DataTable dataTable = new DataTable();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        //MessageBox.Show(dataTable.Rows[0][2].ToString());
                        connection.Close();
                        return dataTable;
                    }
                   
                }
                // return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        // search send parameter cate name  return proId
        public string getItemProductId(string proName)
        {
            string result = "";
            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  productId from products where productName='" + proName + "'";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        //   LenghtData;
                        // MessageBox.Show(dataTable.Rows.Count+"");
                        result = dataTable.Rows[0][0].ToString();
                        connection.Close();
                        //MessageBox.Show(CateID);

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }
        // insert History stock method
        public void InsertHistoryStock(string stockId, string cateId, string proId, string unit, string date, string quantity, string employee, string bal,string status)
        {
            try
            {
                var sql = "insert into HrStock values(@stockId , @categoryId ,@productId ,@unit , @stockDate , @Quantity ,@employeeId , @bal ,@status)";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    HrStockEntiry stock = new HrStockEntiry(stockId, cateId, proId, unit, date, quantity, employee, bal,status);
                    var result = con.Execute(sql, stock);
                    con.Close();
                   // MessageBox.Show("Save History Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //remove history stock method
        public void RemoveHistoryStock(string stockId)
        {
            try
            {
                var sql = "delete from HrStock where stockID='" + stockId + "'";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    // StockEntity stock = new StockEntity(stockId, cateId, proId, unit, date, quantity, employee, bal);
                    var result = con.Execute(sql);
                    con.Close();
                    MessageBox.Show("Remove History and Restor Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //get data history stock to datagrid
        public DataTable GetDataHistoryStock(string startdate , string enddate)
        {
            var table = new DataTable();
            try
            {
                // join table stock and category , employee , products
                var sql = "select * from Hrstock where stockDate between '"+startdate+"' and '"+enddate+"'";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    using (var cmd = new SqlCommand(sql, con))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(table);
                        con.Close();
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Get data Error");
            }
            return null;
        }
        //get Item stock and add to history stock
        public void SaveHistoryStock(string stockId)
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from Stock where stockID='"+stockId+"'";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);

                        string stcId = dataTable.Rows[0][0].ToString();
                        string cateId = dataTable.Rows[0][1].ToString();
                        string proId = dataTable.Rows[0][2].ToString();
                        string unit = dataTable.Rows[0][3].ToString();
                        string date = dataTable.Rows[0][4].ToString();
                        string quantity = dataTable.Rows[0][5].ToString();
                        string emId = dataTable.Rows[0][6].ToString();
                        string bal = dataTable.Rows[0][7].ToString();
                        connection.Close();
                        //  string status = dataTable.Rows[0][8].ToString();
                        //var datadate = new DateTime(Convert.ToInt16(date));
                        InsertHistoryStock(stcId , cateId,proId,unit,date,quantity,emId,bal,"Delete");

                        //   LenghtData;
                        // MessageBox.Show(dataTable.Rows.Count+"");


                    }
                }
                // return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //Cart Management
        //Save data to Cart Table
        public void InserCart(string transactionno , string productId , double price, string quantity,double discount, double total, string date, string status)
        {
            try
            {
                var sql = "insert into Cart values(@transactionno ,@productId ,@price ,@quantity ,@discount ,@total,@date ,@status)";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    CartEntity cart = new CartEntity(Convert.ToInt16(0),transactionno,productId,price,quantity,discount,total,date,status);
                    var result = con.Execute(sql, cart);
                    con.Close();
                    // MessageBox.Show("Save History Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }
        //Cart Id to Gridview
        public DataTable DataCart()
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from Cart_Product";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString()+"class");
            }
            return null;
        }

        //Cart Id to Gridview
        public DataTable getCartProductName(string tranId)
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select TransactionNo , productName , Price , Discount , Quantity from Cart_Product where TransactionNo='" + tranId+"' ";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "class");
            }
            return null;
        }

        //Remove Item as Cart
        public void RemoveCart(string tranNo)
        {
            try
            {
                var sql = "delete from Cart where transactionNo='"+tranNo+"'";
                using (var con = new SqlConnection(conn.getConnection()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    var result = con.Execute(sql);
                    con.Close();
                    //MessageBox.Show("Remove Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "");
            }
        }

        //search  sell ietm in cart product
        public DataTable searchCart(string tranID)
        {

            DataTable dataTable = new DataTable();
            try
            {
                string sql = "select  * from Cart where transactionNo ='" + tranID+"' ";
                using (var connection = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString() + "class");
            }
            return null;
        }

        //get total price in  cart
        public DataTable getTotalAmont(string tranID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                var sql = "select SUM(Total) from Cart where transactionNo='"+tranID+"' ";
                using (var connection = new SqlConnection(conn.getConnection()))
                {
                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    //
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                        dataAdapter.Fill(dataTable);
                        connection.Close();
                    }
                    
                }
                return dataTable;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        public IEnumerable<EmployeeEntity> getEmployee()
        {
            IEnumerable<EmployeeEntity> result = null;
            try
            {
                var sql = "select * from Employee";
                using (var con = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();

                     result = con.Query<EmployeeEntity>(sql);
                    con.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return result;
        }

        //insert to table employee
        public void addItemEmployee(string employeeId, string employeeName)
        {
            try
            {
                var sql = "insert into Employee values(@employeeId,@employeeName)";
                using (var con = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    
                    var em = new EmployeeEntity(Convert.ToInt32(null), employeeId, employeeName);
                    con.Execute(sql,em);
                    MessageBox.Show("Save Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //update to table employee
        public void UpdateItemEmployee(string id,string emId, string name)
        {
            try
            {
                var sql = "update  Employee set EmployeeId=@employeeId ,EmployeeName=@employeeName where ID=@id ";
                using (var con = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                    var em = new EmployeeEntity(Convert.ToInt32(id), emId, name);
                    con.Execute(sql,em);
                    MessageBox.Show("Update Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }


        //remove to table employee
        public void RemoveItemEmployee(string id)
        {
            try
            {
                var sql = "delete from Employee where ID='"+id+"' ";
                using (var con = new SqlConnection(conn.getConnection().ToString()))
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();


                   
                    con.Execute(sql);
                    MessageBox.Show("Remove Successful...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }

   
}
