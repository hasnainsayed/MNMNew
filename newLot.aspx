<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newLot.aspx.cs" Inherits="newLot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Bora  
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Masters</li>
                <li class="active"><a  href="newLot.aspx">Bora</a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <div class="box box-primary" id="divGBora" runat="server" visible="false">
                                        <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
               
                         <div class="row">

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vendor </label>
                                    <asp:DropDownList ID="vendorID1" runat="server" CssClass="form-control select2" DataTextField="VendorName" DataValueField="VendorID"></asp:DropDownList>
                                </div>

                            </div>

                                <div class="col-md-3">
                                <div class="form-group">
                                    <label>No. of Bora </label>
                                    <asp:TextBox runat="server" ID="noOfBora" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                              <div class="box-body table-responsive col-md-6">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="saveBora"  class="btn btn-success pull-right btn-round" runat="server" Text="Generate Bora" OnClick="saveBora_Click" ValidationGroup="grp" OnClientClick="return confirm('Do you Want to Save ?');"/>
                        <asp:Button ID="Button2"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                             </div></div></div>

                                        </div>
                 <div class="box box-primary" id="devCapone" runat="server" visible="false">
                  <!-- BOx -->
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title"></h3>

                       <%-- <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>

                        </div>--%>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
               
                         <div class="row">

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vendor </label>
                                    <asp:DropDownList ID="vendorID" runat="server" CssClass="form-control select2" DataTextField="VendorName" DataValueField="VendorID"></asp:DropDownList>
                                </div>

                            </div>

                                    <div class="col-md-3">
                                <div class="form-group">
                                    <label>Description </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="BagDescription"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Is required." ForeColor="Red" /><br />
                                  <asp:TextBox ID="BagDescription" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total No. of Pieces </label>    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="totalPiece"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Is required." ForeColor="Red" /><br />
                                  <asp:TextBox ID="totalPiece" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total Amount </label> 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="totalAmount"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Is required." ForeColor="Red" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                                    ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                    ControlToValidate="totalAmount" ValidationGroup="grp" ForeColor="Red" />
                                  <asp:TextBox ID="totalAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Invoice No. </label>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="invoiceNo"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Is required." ForeColor="Red" /><br />
                                  <asp:TextBox ID="invoiceNo" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Invoice Date </label>
                                
                                  <asp:TextBox ID="invoiceDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                </div>

                            </div>

                                  <div class="col-md-3">
                                <div class="form-group">
                                    <label>Upload Image </label>
                                    <asp:FileUpload ID="FileUpload1" runat="server" /><br/>
                                    <asp:Image ID="lotImageDisplay" runat="server" ImageUrl="..." Visible="false" Height="100px" Width="100px" />
                                </div>
                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>LR </label>
                                    <asp:DropDownList ID="lrno" runat="server" CssClass="form-control select2" DataTextField="lrno" DataValueField="id"></asp:DropDownList>
                                </div>

                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Travel Cost </label>
                                    <asp:TextBox runat="server" ID="travelCost" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             

                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="grp" OnClientClick="return confirm('Do you Want to Save ?');"/>
                        <asp:Button ID="btnCancel"  class="btn btn-danger pull-right btn-round" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
                <!-- /.box -->
            </div>
            </div>
     
        
<section class="content">
        <!-- /.box-header -->
        <div class="row">
            <div class="col-xs-12">
               
            <asp:Label ID="hdnID" runat="server" Text="" Visible="false"></asp:Label> 
                
                <div class="box">
                    <div class="box-header">
                        <h3 class="box-title">
                            <asp:Button ID="btnadd" runat="server" Text="Add New Bora" class="btn btn-primary btn-sm" OnClick="btnadd_Click" visible="false"/>
                             <asp:Button ID="generateBora" runat="server" Text="Generate Bora" class="btn btn-primary btn-sm" OnClick="generateBora_Click" />
                            <asp:Button ID="addPayment" runat="server" Text="Add Payment" class="btn btn-warning btn-sm" OnClick="addPayment_Click" />
                            <asp:Button ID="generateMulBora" runat="server" Text="Generate Multiple Bora" class="btn btn-info btn-sm" OnClick="generateMulBora_Click" />
                        </h3>
                          </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <th>ID</th>
                                   <th>Vendor </th>
                                   <th>Total Piece</th>
                                   <th>LOT</th>
                                   <th>LR No</th>
                                   <th>Edit</th>
                                   <th>Change Status</th>
                                   <th>Payment</th>
                                   <th>Delete</th>
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="GV" runat="server" OnItemDataBound="GV_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("BagId")%>
                                            <asp:Label ID="lblBagId" runat="server" Text='<%# Eval("BagId").ToString() %>' Visible="false"></asp:Label> 
                                                <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive").ToString() %>' Visible="false"></asp:Label>
                                            </td>
                                            <td><%# Eval("VendorName")%></td> 
                                            <td><%# Eval("totalPiece")%></td> 
                                            <td><%# Eval("BagDescription")%></td> 
                                            <td><%# Eval("lrnoname")%></td> 
                                            <td>
                                                <asp:LinkButton ID="edit" Visible="false" runat="server" OnClick="edit_Click" CssClass="btn btn-primary btn-sm"><i class="fa fa-pencil"></i> Edit</asp:LinkButton>
                                               
                                            </td>
                                            <td>
                                                <asp:Label ID="status" runat="server" Visible="false"></asp:Label>
                                                <asp:LinkButton ID="active" runat="server" Visible="false" OnClick="active_Click" CssClass="btn btn-success btn-sm">Active</asp:LinkButton>
                                                <asp:LinkButton ID="inactive" runat="server" Visible="false" OnClick="inactive_Click" CssClass="btn btn-danger btn-sm">InActive</asp:LinkButton>
                                            </td>
                                            <td><asp:LinkButton runat="server" ID="makePayment" CssClass="btn btn-bitbucket btn-sm" OnClick="makePayment_Click"><i class="fa fa-money"></i> Payment</asp:LinkButton></td>
                                            <td><asp:LinkButton runat="server" ID="deleteLot" CssClass="btn btn-danger btn-sm" OnClick="deleteLot_Click"><i class="fa fa-trash"></i> Delete</asp:LinkButton></td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>



                            </tbody>
                                <tfoot>
                            </tfoot>
                        </table>
                         
                    </div>
                    <!-- /.box-body -->
                </div>
   
                <!-- /.box -->
            </div>
            <!-- /.col -->
        </div>
   </section>
    </ContentTemplate>
              <Triggers>
				<asp:PostBackTrigger ControlID="btnSave" />
                 <%--<asp:PostBackTrigger ControlID="btnUpdate" />--%>
                 
			</Triggers>
                            </asp:UpdatePanel>
    </div>
</asp:Content>

