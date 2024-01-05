//
// NorthwindDac.cs
//  : Northwind 를 위한 데이터 액세스 구현
//
using NeoDEEX.Data;
using NeoDEEX.Transactions;
using System.Data;

namespace BizLogicLib;

// Northwind 용 데이터 액세스 컴포넌트 구현
[FoxTransactionController(FoxTransactionControllerKind.RootContext)]
public class NorthwindDac : FoxDacBase, INorthwindDac
{
    public DataSet GetProducts(string? name)
    {
        // 코드를 사용하여 동적 쿼리를 구현한다.
        FoxDbParameterCollection parameters = this.DbAccess.CreateParamCollection();
        string query = "SELECT * FROM products ";
        if (String.IsNullOrEmpty(name) == false)
        {
            query += "WHERE product_name like :productname";
            parameters.AddWithValue("productname", '%' + name + '%');
        }
        return this.DbAccess.ExecuteSqlDataSet(query, parameters);
    }
}
