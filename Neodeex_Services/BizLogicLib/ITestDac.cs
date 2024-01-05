//
// ITestDac.cs
//  : Fox Execution Context 를 위한 NorthwindDac 클래스 인터페이스 선언
//
using System.Data;

namespace BizLogicLib;

// TestDac 클래스를 위한 인터페이스
public interface ITestDac : IDisposable
{
    DataSet GetAllData();
    DataSet GetData(long id);
    void InsertData(long id, long? col1 = null, string? col2 = null);
    void UpdateData(long id, long col1, string col2);
    void DeleteAllData();
    void DeleteData(long id);
}