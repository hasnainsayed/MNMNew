<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="userWork.aspx.cs" Inherits="userWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .card-header {
    background-color:#3c8dbc!important;

}
        .btn-link{color:white;}
        .btn-link:hover {color:white;}
        .btn-link:focus {color:white;}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>User Work Report
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Reports</li>
                <li class="active">User Work Report</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section>
                    <div class="box">
                        <div class="box-body">
                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">User Work Report</h3>

                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <table class="table table-bordered table-striped table-hover">
                                        <tr>
                                            <th>Select User</th>
                                            <th>Date</th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="users" runat="server" CssClass="form-control select2" DataTextField="username" DataValueField="userid"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="dates" runat="server" CssClass="form-control datepicker"></asp:TextBox></td>
                                            <td>
                                                <asp:LinkButton ID="getUserReport" runat="server" OnClick="getUserReport_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> User Report</asp:LinkButton></td>
                                        </tr>
                                    </table>


                                    <div class="accordion" id="accordionExample" runat="server" visible="false">

                                        <div class="card">
                                            <div class="card-header" id="headingOne">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="false" aria-controls="collapseOne">
                                                        Stocked : (<asp:Label runat="server" ID="stockCount" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseOne" class="collapse" aria-labelledby="headingOne" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                        <thead><tr>
                                                            
                                                                <th>SKU</th>
                                                                <th>DateTime</th>
                                                                <th>RackBarcode</th>
                                                                <th>RackDateTime</th>
                                                                <th>Brand</th>
                                                        </tr> </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptStocked" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("BarcodeNo").ToString() %></td>
                                                                        <td><%# Eval("SystemDate").ToString() %></td>
                                                                        <td><%# Eval("RackBarcode").ToString() %></td>
                                                                        <td><%# Eval("RackDate").ToString() %></td>
                                                                        <td><%# Eval("C1Name").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                      <%--  <div class="card">
                                            <div class="card-header" id="headingTwo">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                                        Listed : (<asp:Label runat="server" ID="listedCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                        <thead><tr>
                                                            
                                                                <th>SKU</th>
                                                                <th>DateTime</th>
                                                                <th>List ID</th>
                                                                <th>V Location</th>
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptListed" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("sku").ToString() %></td>
                                                                        <td><%# Eval("recordtimestamp").ToString() %></td>
                                                                        <td><%# Eval("listidgivenbyvloc").ToString() %></td>
                                                                        <td><%# Eval("Location").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>--%>

                                        <div class="card">
                                            <div class="card-header" id="headingThree">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                                        SOLD : (<asp:Label runat="server" ID="soldCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptSold" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("BarcodeNo").ToString() %></td>
                                                                        <td><%# Eval("salesDateTime").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingSix">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseSix" aria-expanded="false" aria-controls="collapseSix">
                                                        Cancelled : (<asp:Label runat="server" ID="cancelledCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseSix" class="collapse" aria-labelledby="headingSix" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                        <thead><tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptCancel" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("BarcodeNo").ToString() %></td>
                                                                        <td><%# Eval("cancelTimeStamp").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingFour">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFour" aria-expanded="false" aria-controls="collapseFour">
                                                        Dispatched : (<asp:Label runat="server" ID="dispatchedCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseFour" class="collapse" aria-labelledby="headingFour" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                        <thead><tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptDispatch" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("BarcodeNo").ToString() %></td>
                                                                        <td><%# Eval("dispatchtimestamp").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingFive">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseFive" aria-expanded="false" aria-controls="collapseFive">
                                                        Return : (<asp:Label runat="server" ID="returnCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseFive" class="collapse" aria-labelledby="headingFive" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>DateTime</th>
                                                                <th>RackBarcode</th>
                                                                <th>RackDateTime</th>
                                                                <th>V Location</th>
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptReturn" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("BarcodeNo").ToString() %></td>
                                                                        <td><%# Eval("returntimestamp").ToString() %></td>
                                                                        <td><%# Eval("RackBarcode").ToString() %></td>
                                                                        <td><%# Eval("RackDate").ToString() %></td>
                                                                        <td><%# Eval("Location").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingSeven">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseSeven" aria-expanded="false" aria-controls="collapseSeven">
                                                        Style : (<asp:Label runat="server" ID="styleCnt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseSeven" class="collapse" aria-labelledby="headingSeven" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>SKU</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptStyle" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("StyleCode").ToString() %></td>
                                                                        <td><%# Eval("SystemDate").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="card">
                                            <div class="card-header" id="headingeight">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseeight" aria-expanded="false" aria-controls="collapseeight">
                                                        Status Control : (<asp:Label runat="server" ID="lblstatuscunt" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseeight" class="collapse" aria-labelledby="headingeight" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>Old Status</th>
                                                                <th>New Status</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rptstatuscontrol" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("barcodeno").ToString() %></td>
                                                                        <td><%# Eval("barcodeoldstus").ToString() %></td>
                                                                        <td><%# Eval("barcodenewstus").ToString() %></td>
                                                                        <td><%# Eval("datetime").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingnine">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapsenine" aria-expanded="false" aria-controls="collapsenine">
                                                        Listed  : (<asp:Label runat="server" ID="lbllisted" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapsenine" class="collapse" aria-labelledby="headingnine" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>SKU</th>
                                                                <th>List ID</th>
                                                                <th>Location</th>
                                                                <th>Type</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="Listedrpt" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("sku").ToString() %></td>
                                                                        <td><%# Eval("listidgivenbyvloc").ToString() %></td>
                                                                        <td><%# Eval("Location").ToString() %></td>
                                                                        <td><%# Eval("type").ToString() %></td>
                                                                        <td><%# Eval("datetime").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card">
                                            <div class="card-header" id="headingten">
                                                <h5 class="mb-0">
                                                    <button class="btn btn-link collapsed" type="button" data-toggle="collapse" data-target="#collapseten" aria-expanded="false" aria-controls="collapsenine">
                                                        Labels  : (<asp:Label runat="server" ID="lblCount" Text=""></asp:Label>)
                                                    </button>
                                                </h5>
                                            </div>
                                            <div id="collapseten" class="collapse" aria-labelledby="headingnine" data-parent="#accordionExample">
                                                <div class="card-body">
                                                    <table class="table table-bordered table-striped table-hover dtSearchDesc">
                                                       <thead> <tr>
                                                            
                                                                <th>Barcode</th>
                                                                <th>Status</th>
                                                                <th>DateTime</th>
                                                            
                                                        </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rpt_Labels" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Eval("barcodeNo").ToString() %></td>
                                                                        <td><%# Eval("labelStatus").ToString() %></td>
                                                                        <td><%# Eval("entryDate").ToString() %></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>

                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

