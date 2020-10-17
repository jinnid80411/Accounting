<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyShopList.aspx.cs" Inherits="Accounting.CompanyShopList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .white_content { 
            display: none; 
            position: absolute; 
            top: 10%; 
            left: 10%; 
            width: 75%; 
            padding: 20px; 
            border: 1px solid gray; 
            border-radius:20px;
            background-color: white; 
            z-index:1002; 
            overflow: auto; 
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
        <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
            <h1 class="h3">店家列表<span id="GroupName"></span><span id="CompanyName"></span>(<span id="ListCount"></span>)</h1>
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
                    <th class="normal_th" scope="col" style="width:10%">店家名稱</th>                
                    <th class="normal_th" scope="col" style="width:25%">地址</th>                    
                    <th class="normal_th" scope="col" style="width:15%">電話</th>                      
                    <th class="normal_th" scope="col" style="width:20%"><a href="javascript:void(0)" onclick="AddShow()">新增</a></th>
                </tr>
            </thead>
            <tbody id="tbody_group">
            </tbody>
        </table>

        <div id="light" class="white_content">
             
                <div class="col-xs-12">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <h4>設定</h4>
                        </div>
                        <div class="panel-body">                            
                            <div class="row">
                                <div class="col-xs-2">
                                </div>
                                <div class="col-xs-4 form-inline" style="padding:5px">
                                    <select size="10" name="lb_select1" multiple="multiple" id="lb_select1" class="form-control" style="width:220px;">
	                                    <!--<option value="D0865BA5">Test</option>
	                                    <option value="3A02530F">林明瑜(處長)</option>
	                                    <option value="8E1D669F">陳婷筠(管理員)</option>
	                                    <option value="C624939E">黃敦群(工程師)</option>-->
                                    </select>
                                </div>
                                <div class="col-xs-2 " style="vertical-align: middle;text-align:center;padding:5px">
                                    <button type="button" id="btn_all_add" onclick="UsersSelect('SelectAll','')" class="btn btn-primary">全部加入 →</button>
                                    <br />
                                    <br />
                                    <button type="button" id="btn_add" onclick="UsersSelect('Select','')" class="btn btn-primary">加入 →</button>
                                    <br />
                                    <br />
                                    <button type="button" id="btn_remove" onclick="UsersSelect('Delete','')" class="btn btn-primary">移除 ←</button>
                                    <br />
                                    <br />
                                    <button type="button" id="btn_all_remove" onclick="UsersSelect('DeleteAll','')" class="btn btn-primary">全部移除 ←</button>
                                </div>
                                <div class="col-xs-4 form-inline" style="padding:5px">
                                    <select size="10" name="lb_select2" multiple="multiple" id="lb_select2" class="form-control" style="width:220px;">
	                                    <!--<option value="D0865BA5">Test</option>
	                                    <option value="3A02530F">林明瑜(處長)</option>
	                                    <option value="8E1D669F">陳婷筠(管理員)</option>
	                                    <option value="C624939E">黃敦群(工程師)</option>-->
                                    </select>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="panel-body text-center">
                                        <button type="button" id="btn_save" class="btn btn-primary">儲存</button>
                                        <button type="button" id="btn_close" class="btn btn-default" onclick="UsersSelect('Close','')">關閉</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </div>

        </main>

    
        <input type="hidden" id="AddStatus" value="" />
        <script type="text/javascript">
        var nodata = "<tr class=\"normal_tr\"><td scope=\"row\" class=\"normal_td\" colspan=\"4\">暫無資料</td></tr>";
        var td_format = "<td scope=\"row\" class=\"normal_td\">{0}</td>";
        var tr_format = "<tr class=\"normal_tr\" id=\"tr_code_{1}\">{0}</tr>";
        var btn_save_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','U')\">更改</a>";
        var btn_cancel_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('GetData','{0}','')\">取消</a>";
        var btn_edit_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('ShowEdit','{0}','')\">編輯</a>";
        var btn_delete_format = "<a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','{0}','D')\">刪除</a>";
        var btn_company_format = "<a href=\"javascript:void(0)\" onclick=\"location.href='CompanyShopList.aspx?cs_code={0}'\">帳號</a>";
        var btn_customer = "<a href=\"javascript:void(0)\" onclick=\"{0}\">{1}</a>";

        var hidden_format = "<input type=\"hidden\" id=\"{0}\" value=\"{1}\">";
        var textbox_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_datepicker_format = "<input type=\"input\" class=\"form-control BDatePicker\" onfocusin=\"BDatePickerByID(this)\" id=\"txb_{1}\" value=\"{0}\" ></input>";
        var textbox_edit_format = "<input type=\"input\" class=\"form-control\" id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var textbox_edit_datepicker_format = "<input type=\"input\" class=\"form-control BDatePicker\" onfocusin=\"BDatePickerByID(this)\"  id=\"txb_{1}_{2}\" value=\"{0}\" ></input>";
        var tr_add_format = "<tr class=\"normal_tr\" id=\"tr_add\">" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","cs_name") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","cs_address") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\">" + String.format(textbox_format,"","cs_telephone") + "</td>" +
            "<td scope=\"row\" class=\"normal_td\"><a href=\"javascript:void(0)\" onclick=\"RunAjax('SendEdit','','C')\">新增</a><a href=\"javascript:void(0)\" onclick=\"AddHide()\">取消</a></td></tr>";

        var a_href = "<a href='{0}' {2}>{1}</a>";

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
            content += String.format(td_format, jsonObj[i].cs_name);
            content += String.format(td_format, jsonObj[i].cs_address);
            content += String.format(td_format, jsonObj[i].cs_telephone);
            var customer_function = "UsersSelect('Open','"+jsonObj[i].cs_code+"')";            
            var button = String.format(btn_edit_format,jsonObj[i].cs_code)+"&nbsp;"+String.format(btn_delete_format,jsonObj[i].cs_code)+"&nbsp;"+String.format(btn_customer,customer_function,"帳號");
            content += String.format(td_format, button);
            //var result = String.format(tr_format, content, jsonObj[i].cs_code);
            $("#tr_code_"+jsonObj[i].cs_code).html(content);
        }

        function ShowEdit(jsonObj)
        {
            var content = "";
            var i = 0;
            var txb_cs_name = String.format(textbox_edit_format, jsonObj[i].cs_name, "cs_name",jsonObj[i].cs_code);
            content += String.format(td_format, txb_cs_name);
            var txb_cs_address = String.format(textbox_edit_format, jsonObj[i].cs_address, "cs_address",jsonObj[i].cs_code);
            content += String.format(td_format, txb_cs_address);
            var txb_cs_telephone = String.format(textbox_edit_format, jsonObj[i].cs_telephone, "cs_telephone",jsonObj[i].cs_code);
            content += String.format(td_format, txb_cs_telephone);
            var button = String.format(btn_save_format,jsonObj[i].cs_code)+"&nbsp;"+String.format(btn_cancel_format,jsonObj[i].cs_code);
            content += String.format(td_format, button);
            $("#tr_code_"+jsonObj[i].cs_code).html(content);
           
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
                    content += String.format(td_format, jsonObj[i].cs_name);
                    content += String.format(td_format, jsonObj[i].cs_address);
                    content += String.format(td_format, jsonObj[i].cs_telephone);
                    var customer_function = "UsersSelect('Open','"+jsonObj[i].cs_code+"')";            
                    var button = String.format(btn_edit_format,jsonObj[i].cs_code)+"&nbsp;"+String.format(btn_delete_format,jsonObj[i].cs_code)+"&nbsp;"+String.format(btn_customer,customer_function,"帳號");
                    content += String.format(td_format, button);
                    result += String.format(tr_format,content,jsonObj[i].cs_code);
                }
                $("#tbody_group").html(result);
                $("#ListCount").html(ListCount.toString());
            }            
        }

        function UsersSelect(Action,code)
        {
            switch (Action)
            {
                case "Open":
                    RunUsers("1","CompanyShop",code)
                    RunUsers("2","CompanyShop",code)
                    $("#light").show();

                    break;

                case "Save":
                    RunUsers("3","CompanyShop",code)
                    $("#light").hide();
                    
                    break;

                case "Close":
                    $("#light").hide();
                    break;

                case "SelectAll":

                    $("#lb_select2").html($("#lb_select2").html() + $("#lb_select1").html());
                    $("#lb_select1").html("");
                    break;

                case "Select":
                    
                    $("#lb_select2").append($("#lb_select1 option:selected"));
                    $("#lb_select1").find(":selected").remove();
                    //$("#lb_select1").remove($("#lb_select1 option:selected"));

                    break;

                case "DeleteAll":
                    
                    $("#lb_select1").html($("#lb_select1").html() + $("#lb_select2").html());
                    $("#lb_select2").html("");
                    break;

                case "Delete":
                    
                    $("#lb_select1").append($("#lb_select2 option:selected"));
                    $("#lb_select2").find(":selected").remove();
                    //$("#lb_select2").remove($("#lb_select2 option:selected"));
                    break;

            }
        }

        function GetUsers(Action,jsonObj)
        {
            var option = "<option value=\"{0}\">{1}</option>";
            var All_option = "";
            
            for (var i = 0; i < jsonObj.length; i++)
            {
                All_option+=String.format(option,jsonObj[i].u_code,jsonObj[i].u_name)
            }
            switch (Action)
            {
                case "1":
                    $("#lb_select1").html(All_option);
                    break;
                case "2":
                    $("#lb_select2").html(All_option);
                    break;
            }
        }

        function RunUsers(Action,type,code)
        {
            var Info = {};
            Info.Action = Action;
            Info.type = type;
            Info.code = code;
            Info.u_code = "";
            
            if (Action == "3")
            {
                var select2 = document.getElementById("lb_select2");
                var u_code = "";
                for (var i = 0; i < select2.options.length; i++)
                {
                    if (u_code != "")
                        u_code += ",";
                    u_code += select2.options[i].value;
                }
                Info.u_code = u_code;
                console.log(u_code);
                    
            }
            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
             $.ajax({
                url: "xml/CompanyUsers.ashx",
                data: jsonData,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    var jsonObj;
                    
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

                                if (Action == "1" || Action == "2") {
                                    if (jsonObj[0].result == "NoData")
                                    {
                                        switch (Action)
                                        {
                                            case "1":
                                                $("#lb_select1").html("");
                                                break;
                                            case "2":
                                                $("#lb_select2").html("");
                                                break;
                                        }
                                    }
                                    else
                                        GetUsers(Action, data);
                                    $("#btn_save").attr("onclick", "UsersSelect('Save','"+code+"')");
                                }

                                if (Action == "3")
                                    alert("編輯成功!");
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

        function RunAjax(Action,cs_code,CRUD)
        {
            var cg_code = getUrlParameter("cg_code");
            var c_code = getUrlParameter("c_code");
            if (c_code.length == 0) {
                alert('請先選擇公司');
                location.href = 'CompanyList.aspx';
            } else {                
                GetGroupName(cg_code);
                GetCompanyName(c_code,cg_code);
            }
            var Info = {};
            Info.Action = Action;
            Info.cg_code = cg_code;
            Info.c_code = c_code;

            switch (Action)
            {
                case "SendEdit":      
                    Info.cs_code = cs_code;              
                    Info.CRUD = CRUD;
                    if (CRUD == "D") {
                        Info.cs_name = "";
                        Info.cs_address = "";
                        Info.cs_telephone = "";
                    }
                    else if (CRUD == "U") {
                        Info.cs_name = $("#txb_cs_name_"+cs_code).val();
                        Info.cs_address = $("#txb_cs_address_"+cs_code).val();
                        Info.cs_telephone = $("#txb_cs_telephone_"+cs_code).val();
                    }
                    else
                    {
                        Info.cs_name = $("#txb_cs_name").val();
                        Info.cs_address = $("#txb_cs_address").val();
                        Info.cs_telephone = $("#txb_cs_telephone").val();
                    }

                    break;
                case "GetData":
                    Info.cs_code = cs_code;
                    Info.CRUD = "";
                    Info.cs_name = "";
                    Info.cs_address = $("#txb_cs_address_"+cs_code).val();
                    Info.cs_telephone = $("#txb_cs_telephone_"+cs_code).val();
                 
                    break;
                case "ShowEdit":
                    Info.cs_code = cs_code;
                    Info.CRUD = "";
                    Info.cs_name = "";
                    Info.cs_address = $("#txb_cs_address_"+cs_code).val();
                    Info.cs_telephone = $("#txb_cs_telephone_"+cs_code).val();
                 
                    if ($("#AddStatus").val() == "Y")
                    {
                        $("#tr_add").remove();
                        $("#AddStatus").val("");
                    }
                    break;
                case "GetAll":
                    Info.cs_code = "";
                    Info.CRUD = "";
                    Info.cs_name = "";
                    Info.cs_address = $("#txb_cs_address_"+cs_code).val();
                    Info.cs_telephone = $("#txb_cs_telephone_"+cs_code).val();
                 
                    break;
                case "GetUsers"://抓取店面帳號
                    Info.cs_code = cs_code;              
                    Info.CRUD = "";
                    Info.cs_name = "";
                    Info.cs_address = "";
                    Info.cs_telephone = "";
                    break;
            }
            
            var jsonData = JSON.stringify(Info);
            
             $.ajax({
                url: "xml/CompanyShopList.ashx",
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
                                    $("#tr_code_" + cs_code).remove();
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

        
        function GetGroupName(cg_code)
        {
            var Info = {};
            Info.Action = "GetData";
            Info.cg_code = cg_code;
            Info.CRUD = "";
            Info.cg_name = "";
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
                            if (jsonObj[0].result == "OK") {

                                var link = String.format(a_href, "CompanyGroupList.aspx?cg_code="+cg_code, jsonObj[0].cg_name, "");
                                $("#GroupName").html("-"+link);
                                
                                console.log("讀取成功");
                            }
                            else if (jsonObj[0].result == "NoData")
                            {
                                $("#GroupName").html("");
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
        function GetCompanyName(c_code,cg_code)
        {
            var Info = {};
            Info.Action = "GetData";
            Info.c_code = c_code; 
            Info.cg_code = cg_code; 
            Info.CRUD = "";
            Info.c_name = "";
            Info.c_id = "";
            Info.c_builddate = "";
            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
             $.ajax({
                url: "xml/CompanyList.ashx",
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
                            if (jsonObj[0].result == "OK") {
                                var link = String.format(a_href, "CompanyList.aspx?cg_code="+cg_code+"&c_code="+c_code, jsonObj[0].c_name, "");                               
                                $("#CompanyName").html("-"+link);
                                
                                console.log("讀取成功");
                            }
                            else if (jsonObj[0].result == "NoData")
                            {
                                $("#CompanyName").html("");
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
