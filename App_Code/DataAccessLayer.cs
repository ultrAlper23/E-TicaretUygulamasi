using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


    public class DataAccessLayer
    {
        //tek tip db ile sql ile bağlantı kurma
        public SqlConnection Connect()
        {
            SqlConnection con = new SqlConnection("workstation id=DBNET11ivan.mssql.somee.com;packet size=4096;user id=benincicavus_SQLLogin_1;pwd=ij3c2zdvyo;data source=DBNET11ivan.mssql.somee.com;persist security info=False;initial catalog=DBNET11ivan");
            con.Open();
            return con;
        }
        //farklı db lerle çalışıyorsak sql ile bağlantı kurma
        public SqlConnection Connect(string dbName)
        {
            SqlConnection con = new SqlConnection(string.Format("server=.; database={0}; integrated security=true", dbName));
            con.Open();
            return con;
        }
        ///////////////////////////////////////////
        //parametre bildirimlerinin oluşturulması//
        ///////////////////////////////////////////

        //isim,değer
        void AddParametres(SqlCommand command, List<Parametreler> list)
        {
            foreach (var parametre in list)
            {
                command.Parameters.AddWithValue(parametre.Name, parametre.Value);
            }
        }

        //isim,tür
        void AddParametresWithType(SqlCommand command, List<Parametreler> list)
        {
            SqlParameter p;
            foreach (var parametre in list)
            {
                p = new SqlParameter(parametre.Name, parametre.Type);
                p.Value = parametre.Value;
                command.Parameters.Add(p);
            }
        }


        //insert-update-delete sorgularını çalıştırma parametresiz
        public int RunASqlStatement(string statement)
        {
            SqlCommand command = new SqlCommand(statement, Connect());

            return command.ExecuteNonQuery();

        }

        //insert-update-delete sorgularını çalıştırma parametreli
        public int RunASqlStatement(string statement, List<Parametreler> list)
        {
            SqlCommand command = new SqlCommand(statement, Connect());
            AddParametres(command, list);//sorguda tanımlanan @p1,@p2, ..... gibi parametre isimlerini ve bu isimlere karşılık gelecek değerleri biraraya getir.
            return command.ExecuteNonQuery();
        }

        //insert-update-delete sorgularını çalıştırma parametresiz türle birlikte
        public int RunASqlStatementWithType(string statement, List<Parametreler> list)
        {
            SqlCommand command = new SqlCommand(statement, Connect());
            AddParametresWithType(command, list);//sorguda tanımlanan @p1,@p2, ..... gibi parametre isimlerini ve bu isimlere karşılık gelecek değerleri biraraya getir.
            return command.ExecuteNonQuery();
        }

        //insert-update-delete sorgularını çalıştırma db parametreli
        public int RunASqlStatement(string statement, string dbName)
        {
            SqlCommand command = new SqlCommand(statement, Connect(dbName));

            return command.ExecuteNonQuery();
        }

        //overload edilebilir.


        ////////////////////
        //select işlemleri//
        ////////////////////

        //tabular parametresiz veriler için
        public DataTable GetDataTable(string statement)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(statement, Connect());
            adap.Fill(dt);

            return dt;//ya 0 satırlık ya da data içeren bir tablo geri döner.
        }

        //tabular parametreli veriler için
        public DataTable GetDataTable(string statement, List<Parametreler> list)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(statement, Connect());
            AddParametres(adap.SelectCommand, list);//adaptor statement ı selectcommand in texti olarak kabul eder.
            adap.Fill(dt);

            return dt;//ya 0 satırlık ya da data içeren bir tablo geri döner.
        }

        //tek satırlı parametresiz veriler için
        public DataRow GetDataRow(string statement)
        {
            DataTable dt = GetDataTable(statement);

            if (dt.Rows.Count != 0)//sorgu sonucunda 1 veya + data satırı elde edilmişse
            {
                return dt.Rows[0];
            }

            else
                return null;//hiçbir sonuç ortaya çıkmıyorsa ortaya çıkan null olduğu için null return edilebilir.
        }

        //tek satırlı parametreli veriler için
        public DataRow GetDataRow(string statement, List<Parametreler> list)
        {
            DataTable dt = GetDataTable(statement, list);

            if (dt.Rows.Count != 0)//sorgu sonucunda 1 veya + data satırı elde edilmişse
            {
                return dt.Rows[0];
            }

            else
                return null;//hiçbir sonuç ortaya çıkmıyorsa ortaya çıkan null olduğu için null return edilebilir.
        }

        //scalar parametresiz veriler için
        public string GetScalar(string statement)
        {
            SqlCommand command = new SqlCommand(statement, Connect());

            return command.ExecuteScalar().ToString();
        }


        //scalar parametreli veriler için
        public string GetScalar(string statement, List<Parametreler> list)
        {
            SqlCommand command = new SqlCommand(statement, Connect());
            AddParametres(command, list);
            return command.ExecuteScalar().ToString();
        }

        //ondalıklı sayılar - db ye aktarırken
        public string ToCurrencyDB(string number)
        {
            return number.Replace(",", ".");
            //***sistem noktalı sayıları , ile yazldığında kabul eder.
            //db ye gönderim de noktalı sayılar sadece noktayla yazılmalı.
        }

        //ondalıklı sayılar - db den çekerken
        public string ToCurrencyCode(string number)
        {
            return number.Replace(".", ",");
            //db den gelecek noktalı sayıları sisteme , ile aktarılsın.
        }

    }


    public class Parametreler
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SqlDbType? Type { get; set; }//SqlDbType non-nullable (yani null değer içeremez) bu nedenle tanımda SqlDbType? 


        public Parametreler()
        {
            Name = null;
            Value = null;
            Type = null;
        }
    }

