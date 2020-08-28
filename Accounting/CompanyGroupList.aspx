<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyGroupList.aspx.cs" Inherits="Accounting.CompanyGroupList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h3">群組列表(<span id="ListCount"></span>)</h1>
            <div class="input-group-append rounded float-right" >

                <input type="text" class="form-control">
                <div class="btn btn-outline-secondary">
                    <svg width="1.5em" height="1.5em" viewBox="0 0 16 16" class="bi bi-search" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" d="M10.442 10.442a1 1 0 0 1 1.415 0l3.85 3.85a1 1 0 0 1-1.414 1.415l-3.85-3.85a1 1 0 0 1 0-1.415z" />
                        <path fill-rule="evenodd" d="M6.5 12a5.5 5.5 0 1 0 0-11 5.5 5.5 0 0 0 0 11zM13 6.5a6.5 6.5 0 1 1-13 0 6.5 6.5 0 0 1 13 0z" />
                    </svg>
                </div>
                <!--<input class="btn btn-outline-secondary" type="button" value="搜尋">-->
                
            </div>

        </div>                        

        <table class="table table-bordered">
            <thead class="thead-light">
                <tr class="normal_tr">                    
                    <th class="normal_th" scope="col" style="width:80px">群組名稱</th>                    
                    <th class="normal_th" scope="col" style="width:20px">管理</th>
                </tr>
            </thead>
            <tbody id="tbody_group">                
            </tbody>
        </table>
        </main>

        <script type="text/javascript">
        //var nodata = "<tr class=\"normal_tr\"><td scope=\"row\" class=\"normal_td\" colspan=\"2\">暫無資料</td></tr>";
        var td_format = "<td scope=\"row\" class=\"normal_td\">{0}</td>";
        var tr_format = "<tr class=\"normal_tr\" id=\"tr_code_{1}\">{0}</tr>";
        var btn_save_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0},'U')\">更改</a>";
        var btn_cancel_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('GetData','{0},'')\">取消</a>";
        var btn_edit_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('ShowEdit','{0},'')\">編輯</a>";
        var btn_delete_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0},'D')\">刪除</a>";
        var hidden_format = "<input type=\"hidden\" id=\"{0}\" value=\"{1}\">";
        $(document).ready(function() {
            RunAjax("GetAll","","");
        });

        function GetData(jsonObj)
        {

        }

        function ShowEdit(jsonObj)
        {

        }

        function GetAll(jsonObj)
        {
            var ListCount = 0;
            if (jsonObj[0].result == "NoData") {
                $("#ListCount").html(ListCount.toString());
                $("#tbody_group").html(nodata);
            } else {
                ListCount = jsonObj.length;
                var result = "";
                for (var i = 0; i < jsonObj.length; i++)
                {
                    var content = "";
                    content += String.format(td_format, jsonObj[i].cg_name);
                    var button = String.format(btn_edit_format,jsonObj[i].cg_code)+String.format(btn_delete_format,jsonObj[i].cg_code);
                    content += String.format(td_format, button);
                    result += String.format(tr_format,content,jsonObj[i].cg_code);
                }
                $("#tbody_group").html(result);
            }
               
            
             
            
        }

        function RunAjax(Action,cg_code,CRUD)
        {
            var Info = {};
            Info.Action = Action;
            switch (Action)
            {
                case "ShowEdit":      
                    Info.cg_code = cg_code;              
                    Info.CRUD = "";
                    Info.cg_name = "";
                    break;
                case "GetData":
                    Info.cg_code = cg_code;
                    Info.CRUD = "";
                    Info.cg_name = "";
                    break;
                case "ShowEdit":
                    Info.cg_code = cg_code;
                    Info.CRUD = "";
                    Info.cg_name = "";
                    break;
                case "GetAll":
                    Info.cg_code = "";
                    Info.CRUD = "";
                    Info.cg_name = "";
                    break;
            }
            var jsonData = JSON.stringify(Info);
             $.ajax({
                url: "xml/CompanyGroupList.ashx",
                data: jsonData,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var jsonObj;
                    var TotalPage;
                    if (typeof (data) == 'undefined' || data == null) {
                        jsonObj = "";
                    }
                    else {
                        jsonObj = data;
                    }
                    console.log(jsonObj);
                    if (jsonObj != "") {
                        if (jsonObj.length > 0) {
                            if (jsonObj[0].result == "OK" || jsonObj[0].result == "NoData") {
                                RunAction(Action, data);
                                console.log("讀取成功");
                            }
                            else {
                                console.log(jsonObj[0].result);
                                alert('讀取失敗');
                            }
                        }
                    }
                }
            });
        }
        function RunAction(Action,data)
        {
            switch (Action)
            {
                case "ShowEdit":
                    ShowEdit(data);
                    break;
                case "GetData":
                    GetData(data);
                    break;              
                case "SendEdit":
                    GetData(data);
                    break;
                case "GetAll":
                    GetAll(data)
                    break;
            }

        }

    </script>

</asp:Content>
