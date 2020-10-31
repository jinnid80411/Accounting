<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.Master" AutoEventWireup="true" CodeBehind="DayIncome.aspx.cs" Inherits="Accounting.DayIncome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-md-4">
                <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
                    <h1 class="h2">Dashboard</h1>
                    <div class="btn-toolbar mb-2 mb-md-0">

                    </div>
                </div>

                <button type="button" class="btn btn-sm btn-outline-secondary dropdown-toggle">
                    <span data-feather="calendar"></span>
                    This week
                </button>


                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text"style="min-width:75px"  for="inputGroupSelect01">$1000</label>
                    </div>

                    <input type="text" id="input_1000" class="form-control" aria-label="Text input with segmented dropdown button" onchange="daily_sum()">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">元</label>
                    </div>
                </div>

                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text"style="min-width:75px"  for="inputGroupSelect01">$100</label>
                    </div>

                    <input type="text"  id="input_100" class="form-control" aria-label="Text input with segmented dropdown button" onchange="daily_sum()">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">元</label>
                    </div>
                </div>

                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text" style="min-width:75px" for="inputGroupSelect01">$50</label>
                    </div>

                    <input type="text" id="input_50" class="form-control" aria-label="Text input with segmented dropdown button" onchange="daily_sum()">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">元</label>
                    </div>
                </div>

                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text" style="min-width:75px"  for="inputGroupSelect01">$10</label>
                    </div>

                    <input type="text" id="input_10" class="form-control" aria-label="Text input with segmented dropdown button" onchange="daily_sum()">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">元</label>
                    </div>
                </div>

                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text" style="min-width:75px"  for="inputGroupSelect01">$5</label>
                    </div>

                    <input type="text" id="input_5" class="form-control" aria-label="Text input with segmented dropdown button" onchange="daily_sum()">
                    <div class="input-group-prepend">
                        <label class="input-group-text" for="inputGroupSelect01">元</label>
                    </div>
                </div>

                <div class="input-group" style="padding-top:5px">
                    <div class="input-group-prepend">
                        <label class="input-group-text" style="min-width:75px"  for="inputGroupSelect01">總計</label>
                    </div>

                    <input type="text" id="output_sum" class="form-control" aria-label="Text input with segmented dropdown button">
                    <div class="input-group-append">
                        <button class="btn btn-outline-secondary" type="button" onclick="RunAjax('Save')" >送出</button>
                    </div>
                </div>
            </main>
     <script>
        $(document).ready(function() {
            RunAjax("Read");
        });
        function daily_sum()
        {
            var input_1000 = TryParseInt($("#input_1000").val());
            var input_100 = TryParseInt($("#input_100").val());
            var input_50 = TryParseInt($("#input_50").val());
            var input_10 = TryParseInt($("#input_10").val());
            var input_5 = TryParseInt($("#input_5").val());

            $("#output_sum").val((input_1000 + input_100 + input_10 + input_50 + input_5).toString());
        }
        function RunAjax(Action) {

            
            var input_1000 = TryParseInt($("#input_1000").val()).toString();
            var input_100 = TryParseInt($("#input_100").val()).toString();
            var input_50 = TryParseInt($("#input_50").val()).toString();
            var input_10 = TryParseInt($("#input_10").val()).toString();
            var input_5 = TryParseInt($("#input_5").val()).toString();

            
            var Msg = CheckMsg();
            if (Msg != "") {
                alert(Msg);
                return false;
            }

            var Info = {};
            Info.input_1000 = input_1000;
            Info.input_100 = input_100;
            Info.input_50 = input_50;
            Info.input_10 = input_10;
            Info.input_5 = input_5;
            Info.Action = Action;
            


            var jsonData = JSON.stringify(Info);
            console.log(jsonData);
            $.ajax({
                url: "xml/DayInCome.ashx",
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
                                if(Action!="Read")
                                    alert(jsonObj[0].Msg);
                                console.log("讀取成功");
                                $("#input_1000").val(jsonObj[0].input_1000);
                                $("#input_100").val(jsonObj[0].input_100);
                                $("#input_50").val(jsonObj[0].input_50);
                                $("#input_10").val(jsonObj[0].input_10);
                                $("#input_5").val(jsonObj[0].input_5);
                                //location.href = jsonObj[0].url;
                                daily_sum();
                            }
                            else {
                                console.log(jsonObj[0].result);
                                alert(jsonObj[0].Msg);
                            }
                        }
                    }
                }
            });
        }
        function CheckMsg()
        {
            var Msg = "";
            var remainder_input_1000 = TryParseInt($("#input_1000").val()) % 1000;
            var remainder_input_100 = TryParseInt($("#input_100").val()) % 100;
            var remainder_input_50 = TryParseInt($("#input_50").val()) % 50;
            var remainder_input_10 = TryParseInt($("#input_10").val()) % 10;
            var remainder_input_5 = TryParseInt($("#input_5").val()) % 5;

            if (remainder_input_1000 != 0) { Msg += "1000元輸入有誤\n"; }
            if (remainder_input_100 != 0) { Msg += "100元輸入有誤\n"; }
            if (remainder_input_50 != 0) { Msg += "50元輸入有誤\n"; }
            if (remainder_input_10 != 0) { Msg += "10元輸入有誤\n"; }
            if (remainder_input_5 != 0) { Msg += "5元輸入有誤\n"; }
            
            return Msg;
        }
    </script>
</asp:Content>
