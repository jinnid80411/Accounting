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
                        <input class="btn btn-outline-secondary" style="height: fit-content;" type="button" onclick=" RunAjax_Serch()" value="搜尋" />
                    </div>

                </div>

                <table class="table table-bordered">
                    <thead class="thead-light">
                        <tr id="thead_list">
                            <th scope="col" width="5%">序號</th>
                            <th colspan="2" scope="col" width="95%">類別</th>
                            <th> <input type="button" class="btn btn-outline-secondary" onclick="Show_Add()" value="新增類別" /></th>
                        </tr>
                        <tr id="thead_add" style="display:none;">
                            <td></td>
                            <td style="border-right: none";>新增類別</td>
                            <td style="border-right: none";>
                                <input id="input_add" class="form-control" type="text"/>
                            </td>
                            <td align="right" style="border-left: none"; >
                                <input type="button" class="btn btn-outline-secondary" onclick="Run_Add()" value="新增"/>
                                <input type="button" class="btn btn-outline-secondary" value="取消"/>
                            </td>
                        </tr>
                    </thead>
                    <tbody id="tbody1"></tbody>

                </table>
                
                <input type="hidden" id="hid_cs_code" value="<%=cs_code %>" />
                <input type="hidden" id="hid_user" value="<%=UserNo %>" />
            </main>
    
    <script type="text/javascript">
        
      /*var dataRow1 = { No: "1", Type: "豬肉", Contentlist: [{ content: "絞肉", merchant: "朱老闆", merchant_id: "123456" }, { content: "肉片", merchant: "朱老闆", merchant_id: "123456" }] };
      var dataRow2 = { No: "2", Type: "菜", Contentlist: [{ content: "高麗菜", merchant: "菜阿姨", merchant_id: "135790" }, { content: "韭菜", merchant: "菜阿姨", merchant_id: "135790" }] };
      data.push(dataRow1);
      data.push(dataRow2);
      */
      var NewCode = 0;
      //新增用"Add_"+NewCode來判定增加新增 ex:Add_1
      var ItemsTypeList;
      var ItemsList;
      var tableData = "";
      var serchcheck = "";
      var addcheck = 0;
      var open_it_code = "";
      var Add_it_code = new Array();
      var Delete_it_code = new Array();
      var Update_it_code = new Array();
      var Add_i_code = new Array();
      var Delete_i_code = new Array();
      var Update_i_code = new Array();


      $(function() {
          RunAjax_Serch();
      });

      function RunAjax_GetAll()
      {
            var Info = {};
            Info.Action = "GetAll";
            Info.cs_code = $("#hid_cs_code").val();
            Info.createuser = $("#hid_user").val();          
            Info.it_code = "";
            Info.CRUD = "";
            Info.it_name = "";
                          
            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
            //console.log(jsonData);
             $.ajax({
                url: "xml/CompanyShop_ExpendItems.ashx",
                data: jsonData,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                 success: function (data) {                     
                     console.log(data);
                     if (data[0].result == "OK")
                     {
                         ItemsTypeList = data[0].ItemsType;
                         ItemsList = data[0].Items;
                         creatTable();
                     }
                 },
                 error: function (jqXHR, textStatus, errorThrown) {
                    console.log("error");
                    
                 }
            });
      }


      function Show_Add()
      {
          $("#thead_add").toggle();
          //$("#thead_list").toggle();
      }
      
      function Run_Add()
      {
          var value = $("#input_add").val();
          NewCode++;
          var code = "Add_" + NewCode;
          ItemsTypeList.push({ it_code: code, it_name: value });
          console.log(ItemsTypeList);
          //$("#thead_add").toggle();
          //$("#thead_list").toggle();
      }

      function RunAjax_Serch()
      {
          RunAjax_GetAll();
      }

      function creatTable()
      {
        for (var i = 0; i < ItemsTypeList.length; i++)
        {
            tableData += "<tr>";
            tableData += "<td>" + ItemsTypeList[i].it_code + "</td>";
            tableData += "<td colspan=\"2\" style = \"border-right: none\"; >" + ItemsTypeList[i].it_name + "</td>";
            tableData += "<td align=\"right\" style=\"border-left: none\"; >" + "<input type=\"button\" class=\"btn btn-outline-secondary\" onclick=\"\" value=\"修改類別\"/>" + "<input type=\"button\" class=\"btn btn-outline-secondary\" value=\"查看\" onclick=\"ItemsControl('"+ItemsTypeList[i].it_code+"')\" />" + "</td>";
            //alert(serchcheck);
            //對應Serchlist() 顯示品項內容
            var ItemsArray = ItemsList.filter(element => element.it_code == ItemsTypeList[i].it_code);
            if (ItemsArray.length>0) {
                tableData += "<tr class=\"tr_" + ItemsArray.it_code + "\" style=\"display:none;background-color: lightslategray;\">";
                tableData += "<td> </td>";
                tableData += "<td width=\"30%\">" + "品項" + "</td>";
                tableData += "<td width=\"40%\" colspan=\"2\">" + "廠商" + "</td> </tr>";
                for (var j = 0; j < ItemsArray.length; j++) {
                    tableData += "<tr class=\"tr_" + ItemsTypeList[i].it_code + "\"  style=\"display:none;\" > <td> </td>" + "<td>" + ItemsArray[j].i_name + "</td>" + "<td style = \"border-right: none\";>" + ItemsArray[j].vendor_name + "</td>";
                    tableData += "<td style = \"border-left: none\"; align=\"right\"><input type=\"button\" class=\"btn btn-outline-secondary\" value=\"修改品項\"/>" + "</td> </tr>";
                }
                //alert(tableData);
            } else {
                tableData += "<tr class=\"tr_" +  ItemsTypeList[i].it_code + "\" style=\"display:none;background-color: lightslategray;\">";
                tableData += "<td> </td>";
                tableData += "<td width=\"30%\">" + "品項" + "</td>";
                tableData += "<td width=\"40%\" colspan=\"2\">" + "廠商" + "</td> </tr>";
                tableData += "<tr class=\"tr_" +  ItemsTypeList[i].it_code + "\" style=\"display:none;\">";
                tableData += "<td colspan=\"4\">暫無資料 </td>";
                tableData += "</tr>";
            }
              
                tableData += "<tr class=\"tr_" +  ItemsTypeList[i].it_code + "\" style=\"display:none;\">";
                tableData += "<td colspan=\"4\" align=\"right\">" + "<input type=\"button\" class=\"btn btn-outline-secondary\" value=\"新增品項\"/>" + "</td>";
                tableData += "</tr>";
                tableData += "</tr>";


              tableData += "</tr>";
        }


        

          $("#tbody1").html(tableData);

          if (open_it_code != "")
              ItemsControl(open_it_code);
      }
        function ItemsControl(it_code)
        {
            if (open_it_code != it_code)
                open_it_code = it_code;
            else
                open_it_code = "";
            $(".tr_" + it_code).toggle();
        }

        //依選擇的No顯示下拉式table
        function Serchlist(i)
        {
            //alert(data[i].Contentlist[0].content);
            serchcheck = data[i].it_code;
            //alert(serchcheck);
            tableData="";
            //creatTable(data);
        }

        
    </script>

</asp:Content>
