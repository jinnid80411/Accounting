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
                    <th class="normal_th" scope="col" style="width:80%">群組名稱</th>                    
                    <th class="normal_th" scope="col" style="width:20%"><a href="javascript:void(0)" onclick="AddShow()">新增</a></th>
                </tr>
            </thead>
            <tbody id="tbody_group">
            </tbody>
        </table>
        </main>

        <input type="hidden" id="AddStatus" value="" />
        <script type="text/javascript">

        var nodata = "<tr class=\"normal_tr\"><td scope=\"row\" class=\"normal_td\" colspan=\"2\">暫無資料</td></tr>";
        var td_format = "<td scope=\"row\" class=\"normal_td\">{0}</td>";
        var tr_format = "<tr class=\"normal_tr\" id=\"tr_code_{1}\">{0}</tr>";
        var btn_save_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','U')\">更改</a>";
        var btn_cancel_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('GetData','{0}','')\">取消</a>";
        var btn_edit_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('ShowEdit','{0}','')\">編輯</a>";
        var btn_delete_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','D')\">刪除</a>";
        var hidden_format = "<input type=\"hidden\" id=\"{0}\" value=\"{1}\">";
        var textbox_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_edit_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var tr_add_format = "<tr class=\"normal_tr\" id=\"tr_add\">" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","cg_name") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\"><a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','','C')\">新增</a><a href=\"javascript:void(0)\" onclick=\"AddHide()\">取消</a></td></tr>";

        $(document).ready(function () {
            RunAjax("GetAll","","");
        });

        function AddShow()
        {
            if ($("#AddStatus").val() != "Y")
            {
                $("#AddStatus").val("Y");
                if ($("#ListCount").html() == "0")
                {
                    $("#tbody_group").html(tr_add_format);
                }
                else
                {
                    var htmlresult = $("#tbody_group").html() + tr_add_format;
                    $("#tbody_group").html(htmlresult);
                }
            }
        }

        function AddHide()
        {
            if ($("#AddStatus").val() == "Y")
            {
                $("#AddStatus").val("");
                if ($("#ListCount").html() == "0") {

                    RunNoData();
                } else {

                    $("#tr_add").remove();
                }
            }
        }

        function GetData(jsonObj)
        {
            var content = "";
            var i = 0;
            content += String.format(td_format, jsonObj[i].cg_name);
            var button = String.format(btn_edit_format,jsonObj[i].cg_code)+String.format(btn_delete_format,jsonObj[i].cg_code);
            content += String.format(td_format, button);
            //var result = String.format(tr_format, content, jsonObj[i].cg_code);
            $("#tr_code_"+jsonObj[i].cg_code).html(content);
        }

        function ShowEdit(jsonObj)
        {
            var content = "";
            var i = 0;
            var txb_cg_name = String.format(textbox_edit_format, jsonObj[i].cg_name, "cg_name",jsonObj[i].cg_code);
            content += String.format(td_format, txb_cg_name);
            var button = String.format(btn_save_format,jsonObj[i].cg_code)+String.format(btn_cancel_format,jsonObj[i].cg_code);
            content += String.format(td_format, button);
            $("#tr_code_"+jsonObj[i].cg_code).html(content);
           
        }

        function GetAll(jsonObj)
        {
            var ListCount = 0;
            if (jsonObj[0].result == "NoData") {
               $("#ListCount").html(ListCount.toString());
                RunNoData();
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
                $("#ListCount").html(ListCount.toString());
            }
            
               
            
             
            
        }

        function RunAjax(Action,cg_code,CRUD)
        {
            var Info = {};
            Info.Action = Action;
            switch (Action)
            {
                case "SendEdit":      
                    Info.cg_code = cg_code;              
                    Info.CRUD = CRUD;
                    if (CRUD == "D") {
                        Info.cg_name = "";
                    }
                    else if (CRUD == "U") {
                        Info.cg_name = $("#txb_cg_name_"+cg_code).val();
                    } else { Info.cg_name = $("#txb_cg_name").val(); }

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
                    if ($("#AddStatus").val() == "Y")
                    {
                        $("#tr_add").remove();
                        $("#AddStatus").val("");
                    }
                    break;
                case "GetAll":
                    Info.cg_code = "";
                    Info.CRUD = "";
                    Info.cg_name = "";
                    break;
            }
            
            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
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
                                if (CRUD == "C") {
                                    var ListCount = parseInt($("#ListCount").html()) + 1;
                                    $("#ListCount").html(ListCount.toString());
                                    AddHide();
                                    RunAction(Action, data, CRUD);
                                } else if (CRUD == "D") {

                                    var ListCount = parseInt($("#ListCount").html()) - 1;
                                    $("#ListCount").html(ListCount.toString());
                                    $("#tr_code_" + cg_code).remove();
                                    RunNoData();
                                }
                                else {
                                    RunAction(Action, data, CRUD);
                                }
                                
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
        function RunNoData()         
        {
            if ($("#AddStatus").val() != "Y" && $("#ListCount").html() == "0")
                 $("#tbody_group").html(nodata);
                
        }

        function RunAction(Action,data,CRUD)
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
                    switch (CRUD)
                    {
                        case "C":
                            GetAll(data);
                            break;
                        case "U":
                            GetData(data);
                            break;
                        case "D":
                            //GetAll(data);
                            break;
                    }

                   
                    break;
                case "GetAll":
                    GetAll(data);
                    break;
            }

        }

    </script>

</asp:Content>
