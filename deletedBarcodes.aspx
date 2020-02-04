<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="deletedBarcodes.aspx.cs" Inherits="deletedBarcodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Deleted Barcode Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="#.aspx">Reports</a></li>
                <li class="active"><a href="deletedBarcodes.aspx">Deleted Barcode Report</a></li>

            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>


                <section class="content">
                    <!-- /.box-header -->
                    <div class="row">
                        <div class="col-xs-12">



                            <div class="box">
                                 
                                <div class="box-body">
                                    <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                                         <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                    <div id="divSucc" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>



                                   <div>
                                       <table class="table table-bordered table-stripped">
                                         
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="salesCheck"/> Sales ID</th>
                                               <td><asp:TextBox ID="salesid" runat="server" CssClass="form-control"></asp:TextBox></td>
                                               <th><asp:CheckBox runat="server" ID="barcodeCheck"/> Barcode</th>
                                               <td><asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="dateRange" Checked="true"/> Date Range</th>
                                               <td><asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="false"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                               <td><asp:LinkButton ID="searchBarcodesDel" runat="server" OnClick="searchBarcodesDel_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                           </tr>
                                          
                                       </table>
                                   </div>

                                    <div class="table-responsive col-md-12 col-xs-12">
                                        <table class="table table-bordered table-striped table-hover dtSearch">
                                            <thead>
                                            <tr>
                                                <th>DeleteDate</th>
                                                <th>Barcode</th>
                                                <th>DeleteID</th>
                                                <th>DeletedFrom</th>
                                                <th>CancelDelRemarks</th>
                                                <th>SalesDate</th>
                                                <th>SalesID</th>
                                                <th>SalesLoc</th>
                                                <th>SalesABWNo</th>
                                                <th>SalesCourier</th>
                                                <th>SoldBy</th>
                                                <th>DispatchDate</th>
                                                <th>DispatchedBy</th>
                                                <th>ReturnDate</th>
                                                <th>ReturnABWNo</th>
                                                <th>ReturnCourier</th>                                   
                                                <th>ReturnReason</th>
                                                <th>ReturnRemarks</th>
                                                <th>ReturnedBy</th>
                                                                              
                                                
                                            </tr>
                                                </thead>
                                            <tbody>
                                            <asp:Repeater runat="server" ID="del_rpt">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("deltimestamp").ToString() %></td>
                                                        <td><%# Eval("BarcodeNo").ToString() %></td>                                                        
                                                        <td><%# Eval("username").ToString() %></td>
                                                        <td><%# Eval("msg").ToString() %></td>
                                                         <td><%# Eval("delreason").ToString() %></td>
                                                        <td><%# Eval("salesDateTime").ToString() %></td>                                                       
                                                        <td><%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                        <td><%# Eval("Location").ToString() %></td>
                                                        <td><%# Eval("salesAbwno").ToString() %></td>
                                                        <td><%# Eval("salesCourier").ToString() %></td>
                                                        <td><%# Eval("salesUser").ToString() %></td>
                                                        <td><%# Eval("dispatchtimestamp").ToString() %></td>
                                                        <td><%# Eval("dispatchUser").ToString() %></td>
                                                        <td><%# Eval("returntimestamp").ToString() %></td>
                                                        <td><%# Eval("returnAbwno").ToString() %></td>
                                                        <td><%# Eval("retCourier").ToString() %></td>                                            
                                                        <td><%# Eval("creasons").ToString() %></td>
                                                        <td><%# Eval("remarks").ToString() %></td>
                                                        <td><%# Eval("retUser").ToString() %></td>                                                      
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                         </asp:Repeater>
                                            <asp:Repeater runat="server" ID="delcan_rpt">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("deltimestamp").ToString() %></td>
                                                        <td><%# Eval("BarcodeNo").ToString() %></td>                                                        
                                                        <td><%# Eval("username").ToString() %></td>
                                                        <td><%# Eval("msg").ToString() %></td>
                                                         <td><%# Eval("delreason").ToString() %></td>
                                                        <td><%# Eval("salesDateTime").ToString() %></td>       
                                                        <td><%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                        <td><%# Eval("Location").ToString() %></td>
                                                        <td><%# Eval("salesAbwno").ToString() %></td>
                                                        <td><%# Eval("salesCourier").ToString() %></td>
                                                        <td><%# Eval("salesUser").ToString() %></td>
                                                        <td><%# Eval("dispatchtimestamp").ToString() %></td>
                                                        <td><%# Eval("dispatchUser").ToString() %></td>
                                                        <td><%# Eval("returntimestamp").ToString() %></td>
                                                        <td><%# Eval("returnAbwno").ToString() %></td>
                                                        <td><%# Eval("retCourier").ToString() %></td>                                            
                                                        <td><%# Eval("creasons").ToString() %></td>
                                                        <td><%# Eval("remarks").ToString() %></td>
                                                        <td><%# Eval("retUser").ToString() %></td>                                                      
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                         </asp:Repeater>

                                                <asp:Repeater runat="server" ID="pick_rpt">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Eval("deltimestamp").ToString() %></td>
                                                        <td><%# Eval("BarcodeNo").ToString() %></td>                                                        
                                                        <td><%# Eval("username").ToString() %></td>
                                                        <td><%# Eval("msg").ToString() %></td>
                                                         <td><%# Eval("delreason").ToString() %></td>
                                                        <td><%# Eval("salesDateTime").ToString() %></td>       
                                                        <td><%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                        <td><%# Eval("Location").ToString() %></td>
                                                        <td><%# Eval("salesAbwno").ToString() %></td>
                                                        <td><%# Eval("salesCourier").ToString() %></td>
                                                        <td><%# Eval("salesUser").ToString() %></td>
                                                        <td><%# Eval("dispatchtimestamp").ToString() %></td>
                                                        <td><%# Eval("dispatchUser").ToString() %></td>
                                                        <td><%# Eval("returntimestamp").ToString() %></td>
                                                        <td><%# Eval("returnAbwno").ToString() %></td>
                                                        <td><%# Eval("retCourier").ToString() %></td>                                            
                                                        <td><%# Eval("creasons").ToString() %></td>
                                                        <td><%# Eval("remarks").ToString() %></td>
                                                        <td><%# Eval("retUser").ToString() %></td>                                                      
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                         </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>

                                </div>
                                <!-- /.box-body -->

                            </div>
                            <!-- /.box-body -->
                        </div>

                        <!-- /.box -->
                    </div>
                    <!-- /.col -->
                    </div>

                   

                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

