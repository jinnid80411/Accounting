<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="UsersList.aspx.cs" Inherits="Accounting.UsersList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h3">使用者列表(<span id="ListCount"></span>)</h1>
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
                    <th class="normal_th" scope="col" style="width:20%">姓名</th>                    
                    <th class="normal_th" scope="col" style="width:30%">帳號</th>                    
                    <th class="normal_th" scope="col" style="width:15%">密碼</th>                    
                    <th class="normal_th" scope="col" style="width:15%">建立時間</th>                    
                    <th class="normal_th" scope="col" style="width:20%"><a href="javascript:void(0)" onclick="AddShow()">新增</a></th>
                </tr>
            </thead>
            <tbody id="tbody_group">
            </tbody>
        </table>
        </main>

    
        <input type="hidden" id="AddStatus" value="" />
        <script type="text/javascript">
        var nodata = "<tr class=\"normal_tr\"><td scope=\"row\" class=\"normal_td\" colspan=\"5\">暫無資料</td></tr>";
        var td_format = "<td scope=\"row\" class=\"normal_td\">{0}</td>";
        var tr_format = "<tr class=\"normal_tr\" id=\"tr_code_{1}\">{0}</tr>";
        var btn_save_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','U')\">更改</a>";
        var btn_cancel_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('GetData','{0}','')\">取消</a>";
        var btn_edit_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('ShowEdit','{0}','')\">編輯</a>";
        var btn_delete_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','D')\">刪除</a>";
        var btn_company_format = "<a href=\"javascript:void(0)\" onclick=\"location.href='CompanyList.aspx?u_code={0}'\">帳號</a>";

        var hidden_format = "<input type=\"hidden\" id=\"{0}\" value=\"{1}\">";
        var normal_format = "<label id=\"{1}\">{0}</label>";
        var textbox_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_datepicker_format = "<input type=\"input\" class=\"form-control BDatePicker\" onfocusin=\"BDatePickerByID(this)\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_edit_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var textbox_edit_datepicker_format = "<input type=\"input\" class=\"form-control BDatePicker\" onfocusin=\"BDatePickerByID(this)\"  id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var tr_add_format = "<tr class=\"normal_tr\" id=\"tr_add\">" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","u_name") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","u_id") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"123456","u_password") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(normal_format,"","createdate") + "</td>" +
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
            content += String.format(td_format, jsonObj[i].u_name);
            content += String.format(td_format, jsonObj[i].u_id);
            content += String.format(td_format, jsonObj[i].u_password);
            content += String.format(td_format, jsonObj[i].createdate);
            if (jsonObj[i].u_level != "99")
            {
                var button = String.format(btn_edit_format, jsonObj[i].u_code) + "&nbsp;" + String.format(btn_delete_format, jsonObj[i].u_code) + "&nbsp;" + String.format(btn_company_format, jsonObj[i].u_code);
                content += String.format(td_format, button);
            } else
                content += String.format(td_format, "");
            //var result = String.format(tr_format, content, jsonObj[i].u_code);
            $("#tr_code_"+jsonObj[i].u_code).html(content);
        }

        function ShowEdit(jsonObj)
        {
            var content = "";
            var i = 0;
            var txb_c_name = String.format(textbox_edit_format, jsonObj[i].u_name, "u_name",jsonObj[i].u_code);
            content += String.format(td_format, txb_c_name);
            var txb_c_id = String.format(textbox_edit_format, jsonObj[i].u_id, "u_id",jsonObj[i].u_code);
            content += String.format(td_format, txb_c_id);
            var txb_c_address = String.format(textbox_edit_format, jsonObj[i].u_password, "u_password",jsonObj[i].u_code);
            content += String.format(td_format, txb_c_address);
            var txb_c_telephone = String.format(normal_format, jsonObj[i].createdate, "createdate"+jsonObj[i].u_code);
            content += String.format(td_format, txb_c_telephone);           
            var button = String.format(btn_save_format,jsonObj[i].u_code)+"&nbsp;"+String.format(btn_cancel_format,jsonObj[i].u_code);
            content += String.format(td_format, button);
            $("#tr_code_"+jsonObj[i].u_code).html(content);
           
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
                    content += String.format(td_format, jsonObj[i].u_name);
                    content += String.format(td_format, jsonObj[i].u_id);
                    content += String.format(td_format, jsonObj[i].u_password);
                    content += String.format(td_format, jsonObj[i].createdate);
                    if (jsonObj[i].u_level != "99")
                    {
                        var button = String.format(btn_edit_format, jsonObj[i].u_code) + "&nbsp;" + String.format(btn_delete_format, jsonObj[i].u_code) + "&nbsp;" + String.format(btn_company_format, jsonObj[i].u_code);;
                        content += String.format(td_format, button);
                    } else
                        content += String.format(td_format, "");
                    result += String.format(tr_format,content,jsonObj[i].u_code);
                }
                $("#tbody_group").html(result);
                $("#ListCount").html(ListCount.toString());
            }
            
               
            
             
            
        }

        function RunAjax(Action,u_code,CRUD)
        {
            
            var Info = {};
            Info.Action = Action;

            switch (Action)
            {
                case "SendEdit":      
                    Info.u_code = u_code;              
                    Info.CRUD = CRUD;
                    if (CRUD == "D") {
                        Info.u_name = "";
                        Info.u_id = "";
                        Info.u_password = "";
                       
                    }
                    else if (CRUD == "U") {

                        Info.u_code = u_code;
                        Info.u_name = $("#txb_u_name_"+u_code).val();
                        Info.u_id = $("#txb_u_id_"+u_code).val();
                        Info.u_password = $("#txb_u_password_"+u_code).val();
                    }
                    else
                    {
                        Info.u_code = u_code;
                        Info.u_name = $("#txb_u_name").val();
                        Info.u_id = $("#txb_u_id").val();
                        Info.u_password = $("#txb_u_password").val();
                    }

                    break;
                case "GetData":
                    Info.u_code = u_code;
                    Info.CRUD = "";
                    Info.u_name = "";
                    Info.u_id = $("#txb_u_id_"+u_code).val();
                    Info.u_password = $("#txb_u_password_"+u_code).val();
                 
                    break;
                case "ShowEdit":
                    Info.u_code = u_code;
                    Info.CRUD = "";
                    Info.u_name = "";
                    Info.u_id = $("#txb_u_id_"+u_code).val();
                    Info.u_password = $("#txb_u_password_"+u_code).val();
                 
                    if ($("#AddStatus").val() == "Y")
                    {
                        $("#tr_add").remove();
                        $("#AddStatus").val("");
                    }
                    break;
                case "GetAll":
                    Info.u_code = "";
                    Info.CRUD = "";
                    Info.u_name = "";
                    Info.u_id = $("#txb_u_id_"+u_code).val();
                    Info.u_password = $("#txb_u_password_"+u_code).val();
                 
                    break;
            }
            
            var jsonData = JSON.stringify(Info);
            
             $.ajax({
                url: "xml/UsersList.ashx",
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
                                    $("#tr_code_" + u_code).remove();
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
