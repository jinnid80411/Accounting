<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyShop_ItemsList.aspx.cs" Inherits="Accounting.CompanyShop_ItemsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h3">項目列表(<span id="ListCount"></span>)</h1>
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
                    <th class="normal_th" scope="col" style="width:30%">項目名稱</th>
                    <th class="normal_th" scope="col" style="width:30%">廠商</th>
                    <th class="normal_th" scope="col" style="width:20%">單位</th>
                    <th class="normal_th" scope="col" style="width:20%"><a href="javascript:void(0)" onclick="AddShow()">新增</a></th>
                </tr>
            </thead>
            <tbody id="tbody_group">
            </tbody>
        </table>
        </main>

        <input type="hidden" id="AddStatus" value="" />
        <input type="hidden" id="hid_cs_code" value="<%=cs_code %>" />
        <input type="hidden" id="hid_it_code" value="<%=it_code %>" />
        <input type="hidden" id="hid_user" value="<%=UserNo %>" />
        <script type="text/javascript">

        var nodata = "<tr class=\"normal_tr\"><td scope=\"row\" class=\"normal_td\" colspan=\"3\">暫無資料</td></tr>";
        var td_format = "<td scope=\"row\" class=\"normal_td\">{0}</td>";
        var tr_format = "<tr class=\"normal_tr\" id=\"tr_code_{1}\">{0}</tr>";
        var btn_save_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','U')\">更改</a>";
        var btn_cancel_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('GetData','{0}','')\">取消</a>";
        var btn_edit_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('ShowEdit','{0}','')\">編輯</a>";
        var btn_delete_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','D')\">刪除</a>";
        var btn_company_format = "<a href=\"javascript:void(0)\" onclick=\"location.href='CompanyList.aspx?it_code={0}'\">品項</a>";

        var hidden_format = "<input type=\"hidden\" id=\"{0}\" value=\"{1}\">";
        var textbox_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_edit_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var dp_unit = "<%=dp_unit%>";//only {0}=>id  value要用後給
        var tr_add_format = "<tr class=\"normal_tr\" id=\"tr_add\">" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","it_name") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(dp_unit,"dp_unit") + "</td>" +
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

        function GetData(jsonObj,code)
        {
            var content = "";
            var i = 0;
            if (code != "") {
                $("#tr_code_" + code).attr("id","tr_code_"+jsonObj[i].it_code);
            }
           
            content += String.format(td_format, jsonObj[i].it_name);
            content += String.format(td_format, jsonObj[i].ut_name_all);
            var button = String.format(btn_edit_format,jsonObj[i].it_code)+"&nbsp;"+String.format(btn_delete_format,jsonObj[i].it_code)+"&nbsp;"+String.format(btn_company_format,jsonObj[i].it_code);
            content += String.format(td_format, button);
            //var result = String.format(tr_format, content, jsonObj[i].it_code);
            $("#tr_code_" + jsonObj[i].it_code).html(content);

            
        }

        function ShowEdit(jsonObj)
        {
            var content = "";
            var i = 0;
            var txb_it_name = String.format(textbox_edit_format, jsonObj[i].it_name, "it_name",jsonObj[i].it_code);
            content += String.format(td_format, txb_it_name);
            var dp_unit_conrtol = (dp_unit, "dp_unit_" + jsonObj[i].it_code);
            content += String.format(td_format, dp_unit_conrtol);
            var button = String.format(btn_save_format,jsonObj[i].it_code)+"&nbsp;"+String.format(btn_cancel_format,jsonObj[i].it_code);
            content += String.format(td_format, button);
            $("#tr_code_" + jsonObj[i].it_code).html(content);
            $("#dp_unit_" + jsonObj[i].it_code).val(jsonObj[i].ut_code);
           
        }

        function GetAll(jsonObj)
        {
            var it_code = getUrlParameter("it_code");
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
                    content += String.format(td_format, jsonObj[i].it_name);
                    content += String.format(td_format, jsonObj[i].ut_name_all);
                    var button = String.format(btn_edit_format,jsonObj[i].it_code)+"&nbsp;"+String.format(btn_delete_format,jsonObj[i].it_code)+"&nbsp;"+String.format(btn_company_format,jsonObj[i].it_code);;
                    content += String.format(td_format, button);
                    result += String.format(tr_format,content,jsonObj[i].it_code);
                }
                $("#tbody_group").html(result);
                $("#ListCount").html(ListCount.toString());
            }
        }

        function RunAjax(Action,it_code,CRUD)
        {
            /*
            public string Action { set; get; }
            public string cs_code { set; get; }
            public string CRUD { set; get; }
            public string it_name { set; get; }
            public string createuser { set; get; }
            public string it_code { set; get; } 
            public string ut_code { set; get; } 
            */
            var Info = {};
            Info.Action = Action;
            Info.cs_code = $("#hid_cs_code").val();
            Info.createuser = $("#hid_user").val();
            Info.it_code = $("#hid_it_code").val();
            switch (Action)
            {
                case "SendEdit":      
                    Info.i_code = i_code;              
                    Info.CRUD = CRUD;
                    if (CRUD == "D") {
                        Info.it_name = "";           
                        Info.ut_code = "";   
                    }
                    else if (CRUD == "U") {
                        Info.it_name = $("#txb_it_name_" + it_code).val();                              
                        Info.ut_code = $("#dp_unit_" + it_code).val();
                    } else {
                        Info.it_name = $("#txb_it_name").val();                           
                        Info.ut_code = $("#dp_unit").val();
                    }

                    break;
                case "GetData":
                    Info.it_code = it_code;
                    Info.CRUD = "";
                    Info.it_name = "";
                    Info.ut_code = "";
                    break;
                case "ShowEdit":
                    Info.it_code = it_code;
                    Info.CRUD = "";
                    Info.it_name = "";
                    Info.ut_code = "";
                    if ($("#AddStatus").val() == "Y")
                    {
                        $("#tr_add").remove();
                        $("#AddStatus").val("");
                    }
                    break;
                case "GetAll":
                    Info.it_code = "";
                    Info.CRUD = "";
                    Info.it_name = "";
                    Info.ut_code = "";
                    break;
            }
            
            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
             $.ajax({
                url: "xml/CompanyShop_ItemsTypeList.ashx",
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
                                    RunAction(Action, data, CRUD,"");
                                } else if (CRUD == "D") {

                                    var ListCount = parseInt($("#ListCount").html()) - 1;
                                    $("#ListCount").html(ListCount.toString());
                                    $("#tr_code_" + it_code).remove();
                                    RunNoData();
                                }
                                else {
                                    RunAction(Action, data, CRUD,it_code);
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

        function RunAction(Action,data,CRUD,it_code)
        {
            switch (Action)
            {
                case "ShowEdit":
                    ShowEdit(data);
                    break;
                case "GetData":
                    GetData(data,"");
                    break;              
                case "SendEdit":
                    switch (CRUD)
                    {
                        case "C":
                            GetAll(data);
                            break;
                        case "U":
                            GetData(data,it_code);
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
