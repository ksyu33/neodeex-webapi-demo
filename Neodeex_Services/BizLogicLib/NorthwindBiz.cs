//
// NorthwindBiz.cs
//  : Northwind 를 위한 비즈 로직 구현
//
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;
using System.Data;

namespace BizLogicLib;

// Northwind 용 비즈 클래스
[FoxBizClass("Northwind")]
public class NorthwindBiz : FoxBizBase
{
    //
    // NOTE: FoxBizService에 의해 직접 호출되는 비즈 클래스는 ExecuteionContext를 위한
    // 인터페이스 선언 및 구현이 불필요하다.
    //

    // 단순 조회이므로 Suppress 옵션을 사용한다.
    [FoxBizMethod]
    [FoxTransaction(TransactionOption = FoxTransactionOption.Suppress)]
    public DataSet GetProducts(string? name = null)
    {
        using var dac = new NorthwindDac().CreateExecution<INorthwindDac>();
        return dac.GetProducts(name);
    }
}
