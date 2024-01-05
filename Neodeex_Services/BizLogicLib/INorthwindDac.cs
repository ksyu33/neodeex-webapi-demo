//
// INorthwindDac.cs
//  : Fox Execution Context 를 위한 NorthwindDac 클래스 인터페이스 선언
//
using System.Data;

namespace BizLogicLib;

// NorthwindDac 클래스를 위한 인터페이스
public interface INorthwindDac : IDisposable
{
    DataSet GetProducts(string? name);
}