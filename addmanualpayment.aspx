<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addmanualpayment.aspx.cs" Inherits="addmanualpayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div class="content-wrapper">
        <section class="content-header">
            <h1>Add Manaul Entries
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="tickets.aspx">Payment File</a></li>
                <li class="active">Add Manaul Entries</li>
            </ol>
        </section>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <section>
                    <!-- Default box -->
                    <div class="box">
                        <div class="box-body">

                            <!-- BOx -->
                            <div class="box box-primary">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Add Manaul Entries</h3>

                                </div>
                                <div class="box-body">
                                    <div class="row table-responsive" id="mainDiv" runat="server">

                                        
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                
                                                <th></th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <td><asp:DropDownList ID="virtualLocation" AutoPostBack="true" runat="server" CssClass="form-control select2" DataTextField="Location" DataValueField="LocationID"></asp:DropDownList></td>
                                                <td><asp:LinkButton ID="btngo" runat="server" OnClick="btngo_Click" CssClass="btn btn-sm btn-warning"><i class="fa fa-search"></i> Search</asp:LinkButton></td>
                                            </tr>
                                        </table>

                                    </div>
                                  
                                    <div class="row" id="showItem" runat="server" visible="false">
                                        <div class="col-md-12">
                                        <asp:Button ID="btnaddrow" runat="server" Text="Add New" class="btn bg-purple btn-round margin btn-sm" OnClick="btnaddrow_Click" />
                                    </div>
                                          <div class="box-body table-responsive">
                                        <table class="table table-bordered table-striped dtSearch">
                                            <tr>
                                                <th>Sales ID</th>
                                                <th>Barcode</th>
                                                <th>Type/Status</th>
                                                <th>Sp</th>
                                                <th>Order Date</th>
                                                <th>Chennel commision</th>
                                                 <th>Total IGST</th>
                                                <th>Total CGST</th>
                                                <th>Total SGST</th>
                                                <th>Gateway Commision</th>
                                                <th>Logistic</th>
                                                <th>Penalty</th>
                                                <th>Payable Amount</th>
                                                <th>Total Deduction (inc gst)</th>
                                                <th>Total Deduction (exc gst)</th>
                                                <th>TCS (inc gst)</th>
                                                <th>TCS (exc gst)</th>
                                                <th id="action" runat="server" visible="false">Action</th>
                                                
                                            </tr>
                                            <asp:Repeater ID="rptShowItem" runat="server" >
                                                <ItemTemplate>
                                                    <tr>
                                                       <td><asp:TextBox ID="txtsalesid" runat="server" Text='<%# Eval("salesid").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtbarcode" runat="server" Text='<%# Eval("barcode").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txttype" runat="server" Text='<%# Eval("Type").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtsp" runat="server" Text='<%# Eval("Sp").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        
                                                        <td><asp:TextBox ID="txtdate" runat="server" Text='<%# Eval("orderdat").ToString() %>' CssClass="form-control datepicker"></asp:TextBox></td>
                                                         <td><asp:TextBox ID="txtchcomm" runat="server" Text='<%# Eval("channelcomm").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txttotigst" runat="server" Text='<%# Eval("totaligst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txttotcgst" runat="server" Text='<%# Eval("totalcgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txttotsgst" runat="server" Text='<%# Eval("totalsgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtchgate" runat="server" Text='<%# Eval("channelgate").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtlogis" runat="server" Text='<%# Eval("logistic").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtpenalty" runat="server" Text='<%# Eval("penalty").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txtpayamt" runat="server" Text='<%# Eval("payableamt").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txttotaldecincgst" runat="server" Text='<%# Eval("totaldecincgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                         <td><asp:TextBox ID="txttotaldecexcgst" runat="server" Text='<%# Eval("totaldecexcgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txcincgst" runat="server" Text='<%# Eval("tcsincgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:TextBox ID="txcexcgst" runat="server" Text='<%# Eval("tcsexcgst").ToString() %>' CssClass="form-control"></asp:TextBox></td>
                                                        <td><asp:LinkButton ID="btndelete" runat="server" CssClass="btn btn-primary" OnClick="btndelete_Click"><i class="fa fa-trash"></i></asp:LinkButton></td>
                                                        
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                        <div class="box-body table-responsive col-md-12">
                                                            <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                                <asp:Button ID="Cancel" class="btn btn-danger pull-right btn-sm" runat="server" Text="Cancel" OnClick="Cancel_Click" />
                                                                <asp:Button ID="btnSave" class="btn btn-success pull-right btn-sm" runat="server" Text="Save" OnClick="btnSave_Click" />

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
           <%-- <Triggers>
				<asp:PostBackTrigger ControlID="btnSave" />
                
			</Triggers>--%>

        </asp:UpdatePanel>
    </div>
</asp:Content>

