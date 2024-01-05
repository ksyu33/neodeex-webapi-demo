//
// TestBiz.cs
//  : 테스트를 위한 비즈 로직 구현
//
using NeoDEEX.ServiceModel.Services.Biz;
using NeoDEEX.Transactions;

namespace BizLogicLib;

// 테스트 용 비즈 클래스
// NOTE: 로컬 트랜잭션 컨트롤러를 사용하였다.
[FoxBizClass]
[FoxTransactionController(FoxTransactionControllerKind.LocalTransaction)]
public class TestBiz : FoxBizBase
{
    // 트랜잭션을 사용하여 여러 건의 데이터를 추가 한다.
    [FoxBizMethod]
    public void InsertData(params object[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return;
        }
        using ITestDac dac = new TestDac().CreateExecution<ITestDac>();
        foreach (long id in ids.Select(v => (long)v))
        {
            dac.InsertData(id);
        }
    }
}
