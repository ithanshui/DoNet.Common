using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JM.Common.Examples.web
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    public class test : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            var bs = new byte[context.Request.InputStream.Length];
            var l = context.Request.InputStream.Read(bs, 0, bs.Length);
            var par = System.Text.Encoding.UTF8.GetString(bs);
            var result = "<?xml version=\"1.0\"?>"+
"<ReportData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"+
  @"<HasError>false</HasError>
  <Tables>
    <DataTable>
      <Name>ReportControlCoinfig4</Name>
      <Columns>
        <DataColumn>
          <Name>dtStatDate</Name>
          <DisplayName>统计日期</DisplayName>
          <DataType>System.String</DataType>
          <Index>0</Index>
        </DataColumn>
        <DataColumn>
          <Name>iUserNum</Name>
          <DisplayName>扣费用户数</DisplayName>
          <DataType>System.UInt32</DataType>
          <Index>1</Index>
        </DataColumn>
        <DataColumn>
          <Name>iPayment</Name>
          <DisplayName>日扣费额(Q币)</DisplayName>
          <DataType>System.Decimal</DataType>
          <Index>2</Index>
        </DataColumn>
      </Columns>
      <Rows>
        <DataRow>
          <ItemArray>
            <DataItem>
              <ColumnName>dtStatDate</ColumnName>
              <Value>2012-04-11</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iUserNum</ColumnName>
              <Value>2431</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iPayment</ColumnName>
              <Value>440620.9</Value>
            </DataItem>
          </ItemArray>
        </DataRow>
        <DataRow>
          <ItemArray>
            <DataItem>
              <ColumnName>dtStatDate</ColumnName>
              <Value>2012-04-12</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iUserNum</ColumnName>
              <Value>3225</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iPayment</ColumnName>
              <Value>754549.5</Value>
            </DataItem>
          </ItemArray>
        </DataRow>
        <DataRow>
          <ItemArray>
            <DataItem>
              <ColumnName>dtStatDate</ColumnName>
              <Value>2012-04-13</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iUserNum</ColumnName>
              <Value>2123</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iPayment</ColumnName>
              <Value>325090.2</Value>
            </DataItem>
          </ItemArray>
        </DataRow>
        <DataRow>
          <ItemArray>
            <DataItem>
              <ColumnName>dtStatDate</ColumnName>
              <Value>2012-04-14</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iUserNum</ColumnName>
              <Value>2206</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iPayment</ColumnName>
              <Value>321449.4</Value>
            </DataItem>
          </ItemArray>
        </DataRow>
        <DataRow>
          <ItemArray>
            <DataItem>
              <ColumnName>dtStatDate</ColumnName>
              <Value>2012-04-15</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iUserNum</ColumnName>
              <Value>2304</Value>
            </DataItem>
            <DataItem>
              <ColumnName>iPayment</ColumnName>
              <Value>356877.0</Value>
            </DataItem>
          </ItemArray>
        </DataRow>
      </Rows>
    </DataTable>
  </Tables>
</ReportData>";

            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}