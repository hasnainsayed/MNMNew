<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="tickets.aspx.cs" Inherits="tickets" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Tickets
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="tickets.aspx">Tickets</a></li>

            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>


                <section class="content">
                    <!-- /.box-header -->
                    <div class="row">
                        <div class="col-xs-12">



                            <div class="box">
                                  <div class="box-header">
                        <h3 class="box-title pull-right">
                            <asp:Button ID="btnadd" runat="server" Text="Add New Ticket" class="btn btn-primary btn-sm pull-right" OnClick="btnadd_Click" /></h3>
                          </div>
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
                                               <th><asp:CheckBox runat="server" ID="ticketCheck"/> Ticket No</th>
                                               <td><asp:TextBox ID="ticketNo" runat="server" CssClass="form-control"></asp:TextBox></td>
                                               <th><asp:CheckBox runat="server" ID="vLocCheck"/> Virtual Location</th>
                                               <td><asp:DropDownList ID="vLoc" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="salesCheck"/> Sales ID</th>
                                               <td><asp:TextBox ID="salesid" runat="server" CssClass="form-control"></asp:TextBox></td>
                                               <th><asp:CheckBox runat="server" ID="barcodeCheck"/> Barcode</th>
                                               <td><asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="dateRange" Checked="true"/> Date Range</th>
                                               <td><asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                               <td><asp:LinkButton ID="searchTicket" runat="server" OnClick="searchTicket_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                   <asp:LinkButton ID="exportTicket" runat="server" OnClick="exportTicket_Click" CssClass="btn btn-sm btn-info"><i class="fa fa-file"></i> Export</asp:LinkButton>
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
                                                        <thead><tr><th>Issue Date</th><th>Ticket No</th><th>Barcode</th><th>Virtual Location</th><th>SalesID</th><th>Approved</th><th>Reject</th><th>Video</th><th>Images</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Pending" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td>
                                                                           <asp:Label ID="issueDate" runat="server" Visible="false" Text='<%# Convert.ToDateTime(Eval("issueDate")).ToString("dd MMM yyyy") %>' ></asp:Label>
                                                                           <%# Convert.ToDateTime(Eval("issueDate")).ToString("dd MMM yyyy") %>

                                                                           <asp:Label ID="rimage1" runat="server" Visible="false" Text='<%# Eval("rimage1").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage2" runat="server" Visible="false" Text='<%# Eval("rimage2").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage3" runat="server" Visible="false" Text='<%# Eval("rimage3").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage4" runat="server" Visible="false" Text='<%# Eval("rimage4").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage5" runat="server" Visible="false" Text='<%# Eval("rimage5").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage6" runat="server" Visible="false" Text='<%# Eval("rimage6").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage7" runat="server" Visible="false" Text='<%# Eval("rimage7").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage8" runat="server" Visible="false" Text='<%# Eval("rimage8").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage9" runat="server" Visible="false" Text='<%# Eval("rimage9").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rimage10" runat="server" Visible="false" Text='<%# Eval("rimage10").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rVideo1" runat="server" Visible="false" Text='<%# Eval("rVideo1").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="rVideo2" runat="server" Visible="false" Text='<%# Eval("rVideo2").ToString() %>' ></asp:Label>

                                                                       </td>
                                                                       <td>
                                                                           <asp:Label ID="tid" runat="server" Visible="false" Text='<%# Eval("tid").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="ticketNo" runat="server" Visible="false" Text='<%# Eval("ticketNo").ToString() %>' ></asp:Label><%# Eval("ticketNo").ToString() %>
                                                                       </td>
                                                                       <td><asp:Label ID="BarcodeNo" runat="server" Visible="false" Text='<%# Eval("BarcodeNo").ToString() %>' ></asp:Label> <%# Eval("BarcodeNo").ToString() %></td>
                                                                       <td><asp:Label ID="Location" runat="server" Visible="false" Text='<%# Eval("Location").ToString() %>' ></asp:Label> <%# Eval("Location").ToString() %></td>
                                                                       <td><asp:Label ID="salesidgivenbyvloc" runat="server" Visible="false" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' ></asp:Label> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       <td>
                                                                           <asp:LinkButton ID="successTicket" runat="server" CssClass="btn btn-sm btn-success" OnClick="successTicket_Click"><i class="fa fa-thumbs-up"></i> Approved</asp:LinkButton></td>                                                                 
                                                                       <td> <asp:LinkButton ID="rejectTicket" runat="server" CssClass="btn btn-sm btn-danger" OnClick="rejectTicket_Click"><i class="fa fa-thumbs-down"></i> Reject</asp:LinkButton></td>                                                                 
                                                                       <td><asp:LinkButton ID="video" runat="server" CssClass="btn btn-sm btn-warning" OnClick="video_Click"><i class="fa fa-video-camera"></i> Video</asp:LinkButton></td>
                                                                       <td><asp:LinkButton ID="rImages" runat="server" CssClass="btn btn-sm btn-info" OnClick="rImages_Click"><i class="fa fa-file-image-o"></i> Images</asp:LinkButton></td>
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                                </div>


                                                <!-- /.tab-pane -->
                                                <div class="tab-pane" id="success" style="margin-top:30px;">
                                                    <!-- The timeline -->
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>Issue Date</th><th>Ticket No</th><th>Barcode</th><th>Virtual Location</th><th>SalesID</th><th>Approved Date</th><th>Remittance</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Success" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td> <%# Convert.ToDateTime(Eval("issueDate")).ToString("dd MMM yyyy") %></td>
                                                                       <td>
                                                                           <asp:Label ID="tid" runat="server" Visible="false" Text='<%# Eval("tid").ToString() %>' ></asp:Label>
                                                                           <%# Eval("ticketNo").ToString() %>
                                                                       </td>
                                                                       <td> <%# Eval("BarcodeNo").ToString() %></td>
                                                                       <td> <%# Eval("Location").ToString() %></td>
                                                                       <td> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       <td> <%# Convert.ToDateTime(Eval("replyDate")).ToString("dd MMM yyyy") %></td>
                                                                       <td> <%# Eval("remittance").ToString() %></td>
                                                                      
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>

                                                </div>
                                                <!-- /.tab-pane -->

                                                <div class="tab-pane" id="rejected" style="margin-top:30px;">
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>Issue Date</th><th>Ticket No</th><th>Barcode</th><th>Virtual Location</th><th>SalesID</th><th>Reject Date</th><th>Reason</th></tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Rejected" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td> <%# Convert.ToDateTime(Eval("issueDate")).ToString("dd MMM yyyy") %></td>
                                                                       <td>
                                                                           <asp:Label ID="tid" runat="server" Visible="false" Text='<%# Eval("tid").ToString() %>' ></asp:Label>
                                                                           <%# Eval("ticketNo").ToString() %>
                                                                       </td>
                                                                       <td> <%# Eval("BarcodeNo").ToString() %></td>
                                                                       <td> <%# Eval("Location").ToString() %></td>
                                                                       <td> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       <td> <%# Convert.ToDateTime(Eval("replyDate")).ToString("dd MMM yyyy") %></td>
                                                                       <td> <%# Eval("reasons").ToString() %></td>
                                                                      
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
                                                                    <th>Issue Date</th>
                                                                    <td>
                                                                        <asp:Label ID="displayIssueDate" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Ticket No</th>
                                                                    <td>
                                                                        <asp:Label ID="displayTicketNo" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="displayBarcode" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Virtual Location</th>
                                                                    <td><asp:Label ID="displayVloc" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="Dsalesidgivenbyvloc" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                              


                                                            </table>

                                                            <asp:Label ID="displayTicketId" runat="server" Text="" Visible="false"></asp:Label>                                                            
                                                            
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Reply Date </label>
                                                                <asp:TextBox ID="replyDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            </div>

                                                        </div>
														 <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Remittance </label>
                                                                <asp:TextBox ID="remittance" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														<div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Remittance Rec Date</label>
                                                                <asp:TextBox ID="remittanceRecDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnSuccessCancle" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Tickets List" OnClick="btnSuccessCancle_Click" />
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
                                                                    <th>Issue Date</th>
                                                                    <td>
                                                                        <asp:Label ID="displayIssueDateR" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Ticket No</th>
                                                                    <td>
                                                                        <asp:Label ID="displayTicketNoR" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="displayBarcodeR" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Virtual Location</th>
                                                                    <td><asp:Label ID="displayVlocR" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="DsalesidgivenbyvlocR" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                              


                                                            </table>

                                                            <asp:Label ID="displayTicketIdR" runat="server" Text="" Visible="false"></asp:Label>                                                            
                                                            
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Reply Date </label>
                                                                <asp:TextBox ID="replyDateR" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            </div>

                                                        </div>
														 <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Reason </label>
                                                                <asp:TextBox ID="reasons" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnRejectCancle" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Tickets List" OnClick="btnSuccessCancle_Click" />
                                                                <asp:Button ID="btnSaveReject" class="btn btn-success pull-right btn-sm" runat="server" Text="Reject" OnClick="btnSaveReject_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>  
                                   </div>

                                    <div runat="server" id="showVideo" visible="false">
                                       <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Issue Date</th>
                                                                    <td>
                                                                        <asp:Label ID="vIssuedate" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Ticket No</th>
                                                                    <td>
                                                                        <asp:Label ID="vTicketNo" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="vBarcodeNo" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Virtual Location</th>
                                                                    <td><asp:Label ID="vVLoc" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="vSalesID" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                              


                                                            </table>

                                                            <asp:Label ID="vTid" runat="server" Text="" Visible="false"></asp:Label>                                                            
                                                            
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Video 1</label>
                                                                <asp:TextBox ID="rVideo1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														 <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Video 2 </label>
                                                                <asp:TextBox ID="rVideo2" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
														
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="Button1" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Tickets List" OnClick="btnSuccessCancle_Click" />
                                                                <asp:Button ID="saveVideo" class="btn btn-success pull-right btn-sm" runat="server" Text="Save Video" OnClick="saveVideo_Click" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>  
                                   </div>

                                    <div runat="server" id="showImages" visible="false">
                                       <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Issue Date</th>
                                                                    <td>
                                                                        <asp:Label ID="iIssuedate" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Ticket No</th>
                                                                    <td>
                                                                        <asp:Label ID="iTicketNo" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Barcode</th>
                                                                    <td>
                                                                        <asp:Label ID="iBarcodeNo" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Virtual Location</th>
                                                                    <td><asp:Label ID="iVLoc" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Sales ID</th>
                                                                    <td>
                                                                        <asp:Label ID="iSalesID" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                              


                                                            </table>

                                                            <asp:Label ID="iTid" runat="server" Text="" Visible="false"></asp:Label>      
                                                            <asp:Label ID="iSid" runat="server" Text="" Visible="false"></asp:Label> 
                                                            
                                                        </div>
                                                            <div class="col-md-12">
                                                            <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image1 </label></br>
																<asp:image ID="FileUpload1Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image2</label></br>
																<asp:image ID="FileUpload2Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image3</label></br>
																<asp:image ID="FileUpload3Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                                            </div>

                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image4</label></br>
																<asp:image ID="FileUpload4Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload4" runat="server" />
                                                            </div>

                                                        </div>
                                                           

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image5</label></br>
																<asp:image ID="FileUpload5Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload5" runat="server" />
                                                            </div>

                                                        </div>

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image6</label></br>
																<asp:image ID="FileUpload6Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload6" runat="server" />
                                                            </div>

                                                        </div>

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image7</label></br>
																<asp:image ID="FileUpload7Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload7" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image8</label></br>
																<asp:image ID="FileUpload8Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload8" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image9</label></br>
																<asp:image ID="FileUpload9Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload9" runat="server" />
                                                            </div>

                                                        </div>

                                                             <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Image10</label></br>
																<asp:image ID="FileUpload10Display" runat="server" imageurl="..." Visible="false" Height="55px" Width="55px"/></br>
                                                                <asp:FileUpload ID="FileUpload10" runat="server" />
                                                            </div>

                                                        </div>
														
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="Button2" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Tickets List" OnClick="btnSuccessCancle_Click" />
                                                                <asp:Button ID="saveImages" class="btn btn-success pull-right btn-sm" runat="server" Text="Save Images" OnClick="saveImages_Click" />

                                                            </div>
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
            <Triggers>
        <asp:PostBackTrigger ControlID="exportTicket" />
                 <asp:PostBackTrigger ControlID="saveImages" />
    </Triggers>
           
        </asp:UpdatePanel>
    </div>
</asp:Content>

