<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="websiteRefund.aspx.cs" Inherits="websiteRefund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Website Refund
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="websiteRefund.aspx">Website Refund</a></li>

            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
                                               <th><asp:CheckBox runat="server" ID="dateRange" Checked="true"/> Date Range</th>
                                               <td><asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                               <td><asp:LinkButton ID="searchWebRefund" runat="server" OnClick="searchWebRefund_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                               </td>
                                           </tr>
                                          
                                       </table>
                                   </div>

                                    <div class="card" runat="server" id="mainList">
                                        <div class="card-header p-2">
                                            <ul class="nav nav-pills" style="background-color:gainsboro">
                                                <li class="nav-item active"><a class="nav-link active" href="#pending" data-toggle="tab">Pending</a></li>
                                                <li class="nav-item"><a class="nav-link" href="#success" data-toggle="tab">Approved</a></li>
                                                <li class="nav-item"><a class="nav-link" href="#rejected" data-toggle="tab">Reject</a></li>
                                            </ul>
                                        </div>
                                        <!-- /.card-header -->
                                        <div class="card-body">
                                            <div class="tab-content">
                                                <div class="active tab-pane" id="pending" style="margin-top:30px;">
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>Issue Request Date</th><th>Invoice No</th><th>Reason</th><th>Barcode</th><th>Approved</th><th>Reject</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Pending" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td>
                                                                           <asp:Label ID="custRetTime" runat="server" Visible="false" Text='<%# Convert.ToDateTime(Eval("custRetTime")).ToString("dd MMM yyyy") %>' ></asp:Label>
                                                                           <%# Convert.ToDateTime(Eval("custRetTime")).ToString("dd MMM yyyy") %>
                                                                       </td>
                                                                       <td><%# Eval("invoiceid").ToString() %></td>
                                                                       <td>
                                                                           <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("sid").ToString() %>' ></asp:Label><asp:Label ID="returnFrom" runat="server" Visible="false" Text='<%# Eval("returnFrom").ToString() %>' ></asp:Label>
                                                                           <%# Eval("customerRetReason").ToString() %>
                                                                       </td>
                                                                       
                                                                       <td><asp:Label ID="BarcodeNo" runat="server" Visible="false" Text='<%# Eval("BarcodeNo").ToString() %>' ></asp:Label> <%# Eval("BarcodeNo").ToString() %></td>
                                                                       
                                                                       <td>
                                                                           <asp:LinkButton ID="approveRefund" runat="server" CssClass="btn btn-sm btn-success" OnClick="approveRefund_Click"><i class="fa fa-thumbs-up"></i> Approved</asp:LinkButton></td>                                                                 
                                                                       <td> <asp:LinkButton ID="rejectRefund" runat="server" CssClass="btn btn-sm btn-danger" OnClick="rejectRefund_Click"><i class="fa fa-thumbs-down"></i> Reject</asp:LinkButton></td>                                                                 
                                                                       
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                                </div>


                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="success" style="margin-top:30px;">
                                                    <!-- The timeline -->
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>Refund Date</th><th>Invioce No</th><th>Transfer Details</th><th>Refund Details</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Success" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td> <%# Convert.ToDateTime(Eval("entrydatetime")).ToString("dd MMM yyyy") %></td>
                                                                       <td>
                                                                          
                                                                           <%# Eval("invoiceid").ToString() %>
                                                                       </td>
                                                                       <td> <%# Eval("transferDets").ToString() %></td>
                                                                       <td> <%# Eval("refundDets").ToString() %></td>
                                                                      
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>

                                                </div>
                                                <!-- /.tab-pane -->

                                                <div class="tab-pane" id="rejected" style="margin-top:30px;">
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>Refund Date</th><th>Invioce No</th><th>Transfer Details</th><th>Refund Details</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Rejected" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td> <%# Convert.ToDateTime(Eval("entrydatetime")).ToString("dd MMM yyyy") %></td>
                                                                       <td>
                                                                          
                                                                           <%# Eval("invoiceid").ToString() %>
                                                                       </td>
                                                                       <td> <%# Eval("transferDets").ToString() %></td>
                                                                       <td> <%# Eval("refundDets").ToString() %></td>
                                                                      
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                                </div>
                                                <!-- /.tab-pane -->
                                            </div>
                                            <!-- /.tab-content -->
                                        </div>
                                        <!-- /.card-body -->
                                    </div>

                                   <div id="divSubmitError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                   <div runat="server" id="showSuccess" visible="false">
                                       <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Return Request</th>
                                                                    <td>
                                                                        <asp:Label ID="displayReturnReq" runat="server" Text=''></asp:Label>
                                                                        <asp:Label ID="displayReturnFrom" runat="server" Text='' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="displayBarcode" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                                                                         


                                                            </table>

                                                            <asp:Label ID="displaySalesId" runat="server" Text="" Visible="false"></asp:Label>                                                            
                                                            
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Refund Type </label>
                                                                <asp:DropDownList runat="server" ID="refundType" CssClass="form-control">
                                                                    <asp:ListItem Value="1">PayUMoney</asp:ListItem>
                                                                    <asp:ListItem Value="2">Cash</asp:ListItem>
                                                                    <asp:ListItem Value="3">Cheque</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
														 <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Transfer/Cheque Details </label>
                                                                <asp:TextBox ID="transferDets" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														<div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Refund Details</label>
                                                                <asp:TextBox ID="refundDets" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnSuccessCancle" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to List" OnClick="btnSuccessCancle_Click" />
                                                                <asp:Button ID="btnSaveSuccess" class="btn btn-success pull-right btn-sm" runat="server" Text="Success" OnClick="btnSaveSuccess_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>  
                                   </div>

                                    <div runat="server" id="showReject" visible="false">
                                       <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Return Request</th>
                                                                    <td>
                                                                        <asp:Label ID="displayReturnReqR" runat="server" Text=''></asp:Label>
                                                                        <asp:Label ID="displayReturnFromR" runat="server" Text='' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="displayBarcodeR" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                            


                                                            </table>

                                                            <asp:Label ID="displaySalesIdR" runat="server" Text="" Visible="false"></asp:Label>                                                            
                                                            
                                                        </div>
                                                 
														 <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Reason </label>
                                                                <asp:TextBox ID="refundDetsR" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnRejectCancle" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to List" OnClick="btnRejectCancle_Click" />
                                                                <asp:Button ID="btnSaveReject" class="btn btn-success pull-right btn-sm" runat="server" Text="Reject" OnClick="btnSaveReject_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>  
                                   </div>

                                   
                                <!-- /.box-body -->

                            </div>
                            <!-- /.box-body -->
                        </div>

                        <!-- /.box -->
                    </div>
                    
                   

                </section>
            </ContentTemplate>
    
           
        </asp:UpdatePanel>
    </div>
</asp:Content>

