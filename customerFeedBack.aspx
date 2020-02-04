<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="customerFeedBack.aspx.cs" Inherits="customerFeedBack" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Customer Feedback
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active"><a href="customerFeedBack.aspx">Customer Feedback</a></li>

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
                       
                          </div>
                                <div class="box-body">
                                    <div id="divAddAlert" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>

                                         <div id="divError" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
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
                                               <th><asp:CheckBox runat="server" ID="customerCheck"/> Customer Name</th>
                                               <td><asp:TextBox ID="custname" runat="server" CssClass="form-control"></asp:TextBox></td>
                                               <th><asp:CheckBox runat="server" ID="phoneNoCheck"/> Contact No</th>
                                               <td><asp:DropDownList ID="phoneNo" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="salesCheck"/> Sales ID</th>
                                               <td><asp:TextBox ID="salesid" runat="server" CssClass="form-control"></asp:TextBox></td>
                                               <th><asp:CheckBox runat="server" ID="barcodeCheck"/> Barcode</th>
                                               <td><asp:TextBox ID="barcode" runat="server" CssClass="form-control"></asp:TextBox></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="dateFollowup" Checked="true"/>FollowUp Date Range</th>
                                               <td><asp:TextBox ID="fromDateF" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDateF" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                               <td></td>
                                           </tr>
                                           <tr>
                                               <th><asp:CheckBox runat="server" ID="dateRange" Checked="true"/>Dispatch Date Range</th>
                                               <td><asp:TextBox ID="fromDate" CssClass="form-control datepicker" runat="server" AutoPostBack="true"></asp:TextBox></td>
                                               <td><asp:TextBox ID="toDate" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                               <td>
                                                   <asp:LinkButton ID="searchFeedback" runat="server" OnClick="searchFeedback_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton>
                                                   <asp:LinkButton ID="exportFeedback" runat="server" OnClick="exportFeedback_Click" CssClass="btn btn-sm btn-info"><i class="fa fa-file"></i> Export</asp:LinkButton>
                                               </td>
                                           </tr>
                                          
                                       </table>
                                   </div>

                                    <div class="card" runat="server" id="mainList">
                                        <div class="card-header p-2">
                                            <ul class="nav nav-pills" style="background-color:gainsboro">
                                                <li class="nav-item active"><a class="nav-link active" href="#others" data-toggle="tab">Others</a></li>
                                                <li class="nav-item"><a class="nav-link" href="#satisfied" data-toggle="tab">Satisfied</a></li>
                                                <li class="nav-item"><a class="nav-link" href="#disatisfied" data-toggle="tab">Dissatisfied</a></li>
                                                
                                                
                                            </ul>
                                        </div>
                                        <!-- /.card-header -->
                                        <div class="card-body">
                                            <div class="tab-content">

                                                <div class="active tab-pane table-responsive" id="others" style="margin-top:30px;">
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>FollowUp</th><th>DispatchDate</th><th>SalesID</th>
                                                            <th>Customer</th><th>ContactNo</th>
                                                            <th>CustomerStatus</th><th>ContactStatus</th><th>Call</th>
                                                            <th>Title</th><th>Brand</th>
                                                            <th>TrackingNo</th><th>DeliveryStatus</th><th>DeliverDate</th><th>TextMsg</th>
                                                            <th>Call 1</th><th>Call 2</th><th>Call 3</th><th>Call 4</th><th>Call 5</th>                                                            
                                                            
                                                            
                                                               </tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Others" runat="server" OnItemDataBound="rpt_Others_ItemDataBound">
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td><asp:Label ID="follwUpTime" runat="server" Text='<%# Eval("follwUpTime").ToString() %>' ></asp:Label></td>
                                                                       <td><asp:Label ID="dispatchtimestamp" runat="server" Visible="false" Text='<%# Eval("dispatchtimestamp").ToString() %>' ></asp:Label> <%# Convert.ToDateTime(Eval("dispatchtimestamp")).ToString("dd MMM yyyy") %></td>
                                                                       <td><asp:Label ID="salesidgivenbyvloc" runat="server" Visible="false" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' ></asp:Label> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       <td>
                                                                           <asp:Label ID="callingStatus" runat="server" Visible="false" Text='<%# Eval("callingStatus").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("sid").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="callingCount" runat="server" Visible="false" Text='<%# Eval("callingCount").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="custname" runat="server" Visible="false" Text='<%# Eval("custname").ToString() %>' ></asp:Label><%# Eval("custname").ToString() %>
                                                                       </td>
                                                                       <td><asp:Label ID="phoneNo" runat="server" Visible="false" Text='<%# Eval("phoneNo").ToString() %>' ></asp:Label> <%# Eval("phoneNo").ToString() %></td>

                                                                       <td><asp:Label ID="customerStatus" runat="server" Visible="false" Text='<%# Eval("customerStatus").ToString() %>' ></asp:Label> <%# Eval("customerStatus").ToString() %></td>
                                                                       <td><asp:Label ID="contactStatus" runat="server" Visible="false" Text='<%# Eval("contactStatus").ToString() %>' ></asp:Label> <%# Eval("contactStatus").ToString() %></td>
                                                                       <td><asp:LinkButton runat="server" ID="call1" OnClick="call1_Click" CssClass="btn btn-sm bg-olive"><i class="fa fa-phone"></i> Call 1</asp:LinkButton>
                                                                           <asp:LinkButton runat="server" ID="call2" OnClick="call2_Click" CssClass="btn btn-sm bg-purple"><i class="fa fa-phone"></i> Call 2</asp:LinkButton>
                                                                           <asp:LinkButton runat="server" ID="call3" OnClick="call3_Click" CssClass="btn btn-sm bg-maroon"><i class="fa fa-phone"></i> Call 3</asp:LinkButton>
                                                                           <asp:LinkButton runat="server" ID="call4" OnClick="call4_Click" CssClass="btn btn-sm bg-orange"><i class="fa fa-phone"></i> Call 4</asp:LinkButton>
                                                                           <asp:LinkButton runat="server" ID="call5" OnClick="call5_Click" CssClass="btn btn-sm bg-navy"><i class="fa fa-phone"></i> Call 5</asp:LinkButton>
                                                                       </td>
                                                                        <td><asp:Label ID="Title" runat="server" Visible="false" Text='<%# Eval("Title").ToString() %>' ></asp:Label> <%# Eval("Title").ToString() %></td>
                                                                       <td><asp:Label ID="C1Name" runat="server" Visible="false" Text='<%# Eval("C1Name").ToString() %>' ></asp:Label> <%# Eval("C1Name").ToString() %></td>
                                                                       <td><asp:Label ID="salesAbwno" runat="server" Visible="false" Text='<%# Eval("salesAbwno").ToString() %>' ></asp:Label> <%# Eval("salesAbwno").ToString() %></td>
                                                                       <td><asp:Label ID="deliveryStatus" runat="server" Visible="false" Text='<%# Eval("delStatus").ToString() %>' ></asp:Label> <%# Eval("delStatus").ToString() %></td>
                                                                       <td><asp:Label ID="deliveryDate" runat="server" Visible="false" Text='<%# Eval("deliveryDate").ToString() %>' ></asp:Label> <%# Eval("deliveryDate").ToString() %></td>
                                                                       <td><asp:Label ID="smsStatus" runat="server" Visible="false" Text='<%# Eval("smsStatus").ToString() %>' ></asp:Label> <%# Eval("smsStatus").ToString() %></td>
                                                                       <td><asp:Label runat="server" ID="call1Time" Text='<%# Eval("callOneDateime").ToString()+ "- " +Eval("call1user").ToString() %>'></asp:Label>
                                                                           </td>
                                                                       <td><asp:Label runat="server" ID="call2Time"  Text='<%# Eval("callTwoDatetime").ToString()+ "- " +Eval("call2user").ToString() %>'></asp:Label>
                                                                           </td>
                                                                       <td><asp:Label runat="server" ID="call3Time" Text='<%# Eval("callThreeDatetime").ToString()+ "- " +Eval("call3user").ToString() %>'></asp:Label>
                                                                           </td>
                                                                       <td><asp:Label runat="server" ID="call4Time" Text='<%# Eval("callFourDatetime").ToString()+ "- " +Eval("call4user").ToString() %>'></asp:Label>
                                                                           </td>
                                                                       <td><asp:Label runat="server" ID="call5Time" Text='<%# Eval("callFiveDatetime").ToString()+ "- " +Eval("call5user").ToString() %>'></asp:Label>
                                                                           </td>
                                                                       
                                                                       
                                                                      
                                                                       
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                                </div>
                                                <!-- /.tab-pane -->

                                                <div class="tab-pane table-responsive" id="satisfied" style="margin-top:30px;">
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>DispatchDate</th><th>SalesID</th><th>Customer</th><th>ContactNo</th>
                                                            <th>ContactStatus</th>
                                                            <th>Title</th><th>Brand</th><th>TrackingNo</th><th>DeliveryStatus</th><th>DeliverDate</th><th>TextMsg</th>
                                                            <th>Call 1</th><th>Call 2</th><th>Call 3</th><th>Call 4</th><th>Call 5</th>
                                                            </tr></thead>
                                                        <tbody>
                                                            <asp:Repeater ID="rpt_Satisfied" runat="server" >
                                                               <ItemTemplate>
                                                                   <tr>
                                                                       <td><%# Convert.ToDateTime(Eval("dispatchtimestamp")).ToString("dd MMM yyyy") %></td>
                                                                       <td><asp:Label ID="salesidgivenbyvloc" runat="server" Visible="false" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' ></asp:Label> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       
                                                                       <td>
                                                                           <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("dispatchtimestamp").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="ticketNo" runat="server" Visible="false" Text='<%# Eval("custname").ToString() %>' ></asp:Label><%# Eval("custname").ToString() %>
                                                                       </td>
                                                                       <td><asp:Label ID="BarcodeNo" runat="server" Visible="false" Text='<%# Eval("phoneNo").ToString() %>' ></asp:Label> <%# Eval("phoneNo").ToString() %></td>
                                                                        <td><%# Eval("contactStatus").ToString() %></td>
                                                                      
                                                                          
                                                                        <td> <%# Eval("Title").ToString() %></td>
                                                                       <td> <%# Eval("C1Name").ToString() %></td>
                                                                       
                                                                        <td><asp:Label ID="Location" runat="server" Visible="false" Text='<%# Eval("salesAbwno").ToString() %>' ></asp:Label> <%# Eval("salesAbwno").ToString() %></td>
                                                                       <td> <%# Eval("delStatus").ToString() %></td>
                                                                       <td> <%# Eval("deliveryDate").ToString() %></td>
                                                                       <td> <%# Eval("smsStatus").ToString() %></td>
                                                                        <td><asp:Label runat="server" ID="call1Time"  Text='<%# Eval("callOneDateime").ToString()+ "- " +Eval("call1user").ToString() %>'></asp:Label> </td>
                                                                       <td><asp:Label runat="server" ID="call2Time"  Text='<%# Eval("callTwoDatetime").ToString()+ "- " +Eval("call2user").ToString() %>'></asp:Label></td>
                                                                       <td><asp:Label runat="server" ID="call3Time"  Text='<%# Eval("callThreeDatetime").ToString()+ "- " +Eval("call3user").ToString() %>'></asp:Label></td>
                                                                        <td><asp:Label runat="server" ID="call4Time"  Text='<%# Eval("callFourDatetime").ToString()+ "- " +Eval("call4user").ToString() %>'></asp:Label></td>
                                                                        <td><asp:Label runat="server" ID="call5Time"  Text='<%# Eval("callFourDatetime").ToString()+ "- " +Eval("call5user").ToString() %>'></asp:Label></td>
                                                                        
                                                                   </tr>
                                                                   </tr>
                                                                   </ItemTemplate></asp:Repeater>

                                                         </tbody>
                                                    </table>
                                                </div>


                                                <!-- /.tab-pane -->
                                                <div class="tab-pane table-responsive" id="disatisfied" style="margin-top:30px;">
                                                    <!-- The timeline -->
                                                    <table class="table table-bordered table-stripped dtSearch">
                                                        <thead><tr><th>DispatchDate</th><th>SalesID</th><th>Customer</th><th>ContactNo</th>
                                                            <th>ContactStatus</th>
                                                            <th>Title</th><th>Brand</th><th>TrackingNo</th><th>DeliveryStatus</th><th>DeliverDate</th><th>TextMsg</th>
                                                            <th>Call 1</th><th>Call 2</th><th>Call 3</th><th>Call 4</th><th>Call 5</th>
                                                            </tr></thead>
                                                         <tbody>
                                                            <asp:Repeater ID="rpt_Disatisfied" runat="server" >
                                                               <ItemTemplate>
                                                                    <tr>
                                                                       <td><%# Convert.ToDateTime(Eval("dispatchtimestamp")).ToString("dd MMM yyyy") %></td>
                                                                       <td><asp:Label ID="salesidgivenbyvloc" runat="server" Visible="false" Text='<%# Eval("salesidgivenbyvloc").ToString() %>' ></asp:Label> <%# Eval("salesidgivenbyvloc").ToString() %></td>
                                                                       
                                                                       <td>
                                                                           <asp:Label ID="sid" runat="server" Visible="false" Text='<%# Eval("dispatchtimestamp").ToString() %>' ></asp:Label>
                                                                           <asp:Label ID="ticketNo" runat="server" Visible="false" Text='<%# Eval("custname").ToString() %>' ></asp:Label><%# Eval("custname").ToString() %>
                                                                       </td>
                                                                       <td><asp:Label ID="BarcodeNo" runat="server" Visible="false" Text='<%# Eval("phoneNo").ToString() %>' ></asp:Label> <%# Eval("phoneNo").ToString() %></td>
                                                                        <td><%# Eval("contactStatus").ToString() %></td>
                                                                      
                                                                        <td> <%# Eval("Title").ToString() %></td>
                                                                       <td> <%# Eval("C1Name").ToString() %></td>
                                                                       
                                                                        <td><asp:Label ID="Location" runat="server" Visible="false" Text='<%# Eval("salesAbwno").ToString() %>' ></asp:Label> <%# Eval("salesAbwno").ToString() %></td>
                                                                       <td> <%# Eval("delStatus").ToString() %></td>
                                                                       <td> <%# Eval("deliveryDate").ToString() %></td>
                                                                       <td> <%# Eval("smsStatus").ToString() %></td>
                                                                       <td><asp:Label runat="server" ID="call1Time"  Text='<%# Eval("callOneDateime").ToString()+ "- " +Eval("call1user").ToString() %>'></asp:Label> </td>
                                                                       <td><asp:Label runat="server" ID="call2Time"  Text='<%# Eval("callTwoDatetime").ToString()+ "- " +Eval("call2user").ToString() %>'></asp:Label></td>
                                                                       <td><asp:Label runat="server" ID="call3Time"  Text='<%# Eval("callThreeDatetime").ToString()+ "- " +Eval("call3user").ToString() %>'></asp:Label></td>
                                                                        <td><asp:Label runat="server" ID="call4Time"  Text='<%# Eval("callFourDatetime").ToString()+ "- " +Eval("call4user").ToString() %>'></asp:Label></td>
                                                                        <td><asp:Label runat="server" ID="call5Time"  Text='<%# Eval("callFourDatetime").ToString()+ "- " +Eval("call5user").ToString() %>'></asp:Label></td>
                                                                          
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

                                   <div id="divSubmitError" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i></h5>

                                    </div>
                                   <div runat="server" id="showOthers" visible="false">
                                       <div class="row">
                                            <div class="col-xs-12">
                                                <div class="box">
                                                    <div class="box-body table-responsive">
                                                        <div class="col-xs-12">
                                                            <table class="table table-bordered table-striped table-hover">
                                                                <tr>
                                                                    <th>Customer Status</th>
                                                                    <td>
                                                                        <asp:Label ID="callingCount1" Visible="false" runat="server" Text=''></asp:Label>
                                                                        <asp:Label ID="sid1" Visible="false" runat="server" Text=''></asp:Label>
                                                                        <asp:Label ID="customerStatus1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>ContactStatus</th>
                                                                    <td>
                                                                        <asp:Label ID="conStatus1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Dispatch Datetime</th>
                                                                    <td>
                                                                        <asp:Label ID="dispatchtimestamp1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>SalesID</th>
                                                                    <td>
                                                                        <asp:Label ID="salesidgivenbyvloc1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>Customer</th>
                                                                    <td><asp:Label ID="custname1" runat="server" Text=''></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <th>ContactNo</th>
                                                                    <td>
                                                                        <asp:Label ID="phoneNo1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>                                                              
                                                                <tr>
                                                                    <th>Title</th>
                                                                    <td>
                                                                        <asp:Label ID="Title1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr> 
                                                                <tr>
                                                                    <th>Brand</th>
                                                                    <td>
                                                                        <asp:Label ID="C1Name1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr> 
                                                                <tr>
                                                                    <th>TrackingNo</th>
                                                                    <td>
                                                                        <asp:Label ID="salesAbwno1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>DeliveryStatus</th>
                                                                    <td>
                                                                        <asp:Label ID="deliveryStatus1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>DeliveryDate</th>
                                                                    <td>
                                                                        <asp:Label ID="deliveryDate1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <th>TextMsg</th>
                                                                    <td>
                                                                        <asp:Label ID="smsStatus1" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <th>Call History</th>
                                                                    <td>
                                                                        <asp:Label ID="callHistory" runat="server" Text=''></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                                                                               
                                                            
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Contact Status </label>
                                                                <asp:DropDownList ID="conStatus" runat="server" CssClass="form-control">                                                        
                                                                    <asp:ListItem Value="Contacted">Contacted</asp:ListItem>
                                                                    <asp:ListItem Value="Switched Off">Switched Off</asp:ListItem>
                                                                    <asp:ListItem Value="Ring No Answer">Ring No Answer</asp:ListItem>
                                                                    <asp:ListItem Value="Unavailable">Unavailable</asp:ListItem>
                                                                    
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Customer Status </label>
                                                                <asp:DropDownList ID="custStatus" runat="server" CssClass="form-control">
                                                                     <asp:ListItem Value="Satisfied">Satisfied</asp:ListItem>
                                                                    <asp:ListItem Value="Follow-up Required">Follow-up Required</asp:ListItem>
                                                                    <asp:ListItem Value="Dissatisfied">Dissatisfied</asp:ListItem>
                                                                    <asp:ListItem Value="Refund Applied">Refund Applied</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
                                                        
														 <div class="col-md-6" runat="server" ID="delStatusHide">
                                                            <div class="form-group">
                                                                <label>Delivery Status </label>
                                                                <asp:DropDownList ID="delStatus" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="Unshipped">Unshipped</asp:ListItem>
                                                                    <asp:ListItem Value="Waiting For Pickup">Waiting For Pickup</asp:ListItem>
                                                                    <asp:ListItem Value="Picked Up">Picked Up</asp:ListItem>
                                                                    <asp:ListItem Value="Out for Delivery">Out for Delivery</asp:ListItem>
                                                                    <asp:ListItem Value="Delivered to Buyer">Delivered to Buyer</asp:ListItem>
                                                                    <asp:ListItem Value="Rejected by Buyer">Rejected by Buyer</asp:ListItem>
                                                                  
                                                                </asp:DropDownList>
                                                            </div>

                                                        </div>
														<div class="col-md-6" runat="server" ID="delDateHide">
                                                            <div class="form-group">
                                                                <label>Delivery Date</label>
                                                                <asp:TextBox ID="delDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-6" id="follow" runat="server">
                                                            <div class="form-group">
                                                                <label>Follow Up DateTime</label>
                                                                <asp:TextBox ID="followUp" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>Calling Status</label>
                                                                <asp:TextBox ID="callingStatus1" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="btnSuccessCancle" class="btn btn-danger pull-right btn-sm" runat="server" Text="Return to Customer Feedback List" onclick="btnSuccessCancle_Click" />
                                                                <asp:Button ID="btnSaveSuccess" class="btn btn-success pull-right btn-sm" runat="server" Text="" onclick="btnSaveSuccess_Click" />

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
                    <!-- /.col -->
                  

                   

                </section>
            </ContentTemplate>
            <Triggers>
        <asp:PostBackTrigger ControlID="exportFeedback" />
    </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>



