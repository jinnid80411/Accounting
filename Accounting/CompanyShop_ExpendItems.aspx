<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyShop_ExpendItems.aspx.cs" Inherits="Accounting.CompanyShop_ExpendItems" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                
                <div>
                    <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                        <h1 class="h2">支出品項列表</h1>
                    </div>
                    <!-- <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"> -->
                    <div class="input-group-append rounded float-right">

                        <input type="text" id="serch_item" class="form-control" />
                        <!-- <div class="btn btn-outline-secondary" style="height: fit-content;">
                            <svg width="1.5em" height="1.5em" viewBox="0 0 16 16" class="bi bi-search" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" d="M10.442 10.442a1 1 0 0 1 1.415 0l3.85 3.85a1 1 0 0 1-1.414 1.415l-3.85-3.85a1 1 0 0 1 0-1.415z" />
                                <path fill-rule="evenodd" d="M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11zM13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0z" />
                            </svg>
                        </div> -->
                        <input class="btn btn-outline-secondary" style="height: fit-content;" type="button" onclick=" RunAjax_Serch('save')" value="搜尋" />
                    </div>

                </div>

                <table class="table table-bordered">
                    <thead class="thead-light">
                        <tr>
                            <th scope="col" style="width:10%">序號</th>
                            <th style="width:60%" scope="col" >類別</th>
                            <th style="width:20%"> 
                                <input type="button" class="btn btn-outline-secondary" onclick=" RunAjax_Add('add')" value="新增類別" />
                            </th>
                        </tr>
                    </thead>
                    <tbody id="tbody1">
                    </tbody>

                </table>

            </main>
    
    <script type="text/javascript">
      var data = Array();
      var dataRow1 = { No: "1", Type: "豬肉", Contentlist: [{ content: "絞肉", merchant: "朱老闆", merchant_id: "123456" }, { content: "肉片", merchant: "朱老闆", merchant_id: "123456" }] };
      var dataRow2 = { No: "2", Type: "菜", Contentlist: [{ content: "高麗菜", merchant: "菜阿姨", merchant_id: "135790" }, { content: "韭菜", merchant: "菜阿姨", merchant_id: "135790" }] };
      data.push(dataRow1);
      data.push(dataRow2);
      var tableData = "";
      var serchcheck = "";
      var addcheck = 0;

      function RunAjax_Serch(Action)
      {
          var jsonData = JSON.stringify(data);
          var ObjectData = JSON.parse(jsonData);
        tableData = "";
        serchcheck = "";

        creatTable(data);

      }

      function creatTable(data)
      {
        for (var i = 0; i < data.length; i++)
        {

            tableData += "<tr>";
            tableData += "<td>" + data[i].No + "</td>";
            tableData += "<td colspan=\"2\" style = \"border-right: none\"; >" + data[i].Type + "</td>";
            tableData += "<td align=\"right\" style=\"border-left: none\"; >" + "<input type=\"button\" class=\"btn btn-outline-secondary\" value=\"修改類別\"/>" + "<input type=\"button\" class=\"btn btn-outline-secondary\" value=\"查看\" onclick=\"Serchlist(" + i.toString() + ")\" />" + "</td>";
            //alert(serchcheck);
            //對應Serchlist() 顯示品項內容
            if(serchcheck==data[i].No)
            {
              if (data[i].Contentlist != null)
              {
                    tableData += "<tr style=\"background-color: lightslategray;\">";
                    tableData += "<td> </td>";
                    tableData += "<td width=\"30%\">" + "品項" + "</td>";
                    tableData += "<td width=\"40%\"colspan=\"2\">" + "廠商" + "</td> </tr>";
                    for (var j = 0 ; j < data[i].Contentlist.length ; j++) {
                        tableData += "<tr> <td> </td>" + "<td>" + data[i].Contentlist[j].content + "</td>" + "<td style = \"border-right: none\";>" + data[i].Contentlist[j].merchant + "</td>";
                        tableData += "<td style = \"border-left: none\"; align=\"right\"><input type=\"button\" class=\"btn btn-outline-secondary\" value=\"修改品項\"/>" + "</td> </tr>";
                    }
                    tableData += "<tr>";
                    tableData += "<td colspan=\"4\" align=\"right\">" + "<input type=\"button\" class=\"btn btn-outline-secondary\" value=\"新增品項\"/>" + "</td>";
                    tableData += "</tr>";
                    tableData += "</tr>";
                    //alert(tableData);
                }
              }


              tableData += "</tr>";
        }



        $("#tbody1").html(tableData)
        }

        //依選擇的No顯示下拉式table
        function Serchlist(i)
        {
            //alert(data[i].Contentlist[0].content);
            serchcheck = data[i].No;
            //alert(serchcheck);
            tableData="";
            creatTable(data);
        }

    </script>

</asp:Content>
