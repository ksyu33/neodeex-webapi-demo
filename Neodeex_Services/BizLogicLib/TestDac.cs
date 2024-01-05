//
// TestDac.cs
//  : 테스트를 위한 데이터 액세스 구현
//
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;
using System.Data;

namespace BizLogicLib;

// 테스트용 데이터 액세스 컴포넌트 구현
// 주)  트랜잭션 컨트롤러 옵션으로 RootContext 가 사용되었다. 이 옵션은 호출 컴포넌트의
//      트랜잭션 컨트롤러를 사용하거나 수행 문맥이 없는 경우에는 디폴트(FastTransaction) 컨트롤러를
//      사용한다.
[FoxBizClass]
[FoxTransactionController(FoxTransactionControllerKind.RootContext)]
public class TestDac : FoxDacBase, ITestDac
{
    [FoxBizMethod]
    public DataSet GetData(long id)
    {
        string query = "SELECT * FROM TxTestTable WHERE pk = :id";
        var parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        return this.DbAccess.ExecuteSqlDataSet(query, parameters);
    }

    [FoxBizMethod]
    public DataSet GetAllData()
    {
        return this.DbAccess.ExecuteSqlDataSet("SELECT * FROM TxTestTable");
    }

    [FoxBizMethod]
    public void InsertData(long id, long? col1 = null, string? col2 = null)
    {
        col1 ??= id;
        col2 ??= "default value";
        string query = "INSERT INTO TxTestTable(pk, col1, col2) VALUES(:id, :col1, :col2)";
        var parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        parameters.AddWithValue("col1", col1);
        parameters.AddWithValue("col2", col2);
        this.DbAccess.ExecuteSqlNonQuery(query, parameters);
    }

    [FoxBizMethod]
    public void UpdateData(long id, long col1, string col2)
    {
        string query = "UPDATE TxTestTable SET col1 = :col1, col2 = :col2 WHERE pk = :id";
        var parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        parameters.AddWithValue("col1", col1);
        parameters.AddWithValue("col2", col2);
        this.DbAccess.ExecuteSqlNonQuery(query, parameters);
    }

    [FoxBizMethod]
    public void DeleteData(long id)
    {
        string query = "DELETE FROM TxTestTable WHERE pk = :id";
        var parameters = this.DbAccess.CreateParamCollection();
        parameters.AddWithValue("id", id);
        this.DbAccess.ExecuteSqlNonQuery(query, parameters);
    }

    [FoxBizMethod]
    public void DeleteAllData()
    {
        this.DbAccess.ExecuteSqlNonQuery("DELETE FROM TxTestTable");
    }
}
