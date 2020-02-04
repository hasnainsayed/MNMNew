<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="couponListing.aspx.cs" Inherits="couponListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
    <section class="content-header">
            <h1>Coupons</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li>Website</li>
                <li class="active"><a  href="couponListing.aspx">Coupons</a></li>
               
            </ol>
        </section>
     <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <div class="box ">
                                         <div id="divError" runat="server" visible="false" class="alert alert-warning alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
                                        <div id="divSuccess" runat="server" visible="false" class="alert alert-success alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                        <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                    </div>
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

                           
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Coupon Name </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="couponName"
        ValidationGroup="grp" runat="server" ErrorMessage="Coupon Name is required." forecolor="Red" /><br />
                                  <asp:TextBox ID="couponName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                               <div class="col-md-4">
                                <div class="form-group">
                                    <label>Item Category </label>

                                    <asp:DropDownList ID="couponCategory" runat="server" CssClass="form-control select2" DataTextField="ItemCategory" DataValueField="ItemCategoryID"></asp:DropDownList>
                                </div>

                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Valid From </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="validFrom"
        ValidationGroup="grp" runat="server" ErrorMessage="Valid From is required." forecolor="Red" /><br />
                                  <asp:TextBox ID="validFrom" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                </div>

                            </div>
                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Valid To </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="validTo"
        ValidationGroup="grp" runat="server" ErrorMessage="Valid To is required." forecolor="Red" /><br />
                                  <asp:TextBox ID="validTo" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Coupon Type </label>
                                    <asp:DropDownList runat="server" ID="discountOn" CssClass="form-control select2">
                                        <asp:ListItem Value="M">MRP</asp:ListItem>
                                        <asp:ListItem Value="L">Listed Price</asp:ListItem>
                                    </asp:DropDownList>                                  
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Coupon Type </label>
                                    <asp:DropDownList runat="server" ID="couponType" CssClass="form-control select2">
                                        <asp:ListItem Value="P">Percent</asp:ListItem>
                                        <asp:ListItem Value="D">Direct</asp:ListItem>
                                    </asp:DropDownList>                                  
                                </div>

                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Percentage/Amount </label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="couponDiscount"
        ValidationGroup="grp" runat="server" ErrorMessage="Percentage/Amount is required." forecolor="Red" />
                                    <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
ControlToValidate="couponDiscount"  ValidationGroup="grp" forecolor="Red" />
                                  <asp:TextBox ID="couponDiscount" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                           

                            <!-- /.col -->
                    </div>
                    <!-- /.box-body -->

                </div>

                    <div class="box-body table-responsive ">
                    <div class="box-footer btn-toolbar " style="margin-left: 0px;">

                        <asp:Button ID="btnSave"  class="btn btn-success pull-right btn-round" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="grp"/>
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
                            <asp:Button ID="btnadd" runat="server" Text="Add New Coupon" class="btn btn-primary btn-sm" OnClick="btnadd_Click" /></h3>
                          </div>
                    <div class="box-body table-responsive">
                         
                        <table id="example111" class="table table-bordered table-striped dtSearch">
                            <thead>
                               <tr>
                                   <td>ID</td>
                                   <td>Name </td>
                                   <td>Category</td>
                                   <td>Valid From</td>
                                   <td>Valid To</td>
                                   <td>Edit</td>                                   
                               </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rpt_Coupons" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%# Eval("couponId")%><asp:Label ID="couponId" runat="server" Text='<%# Eval("couponId").ToString() %>' Visible="false"></asp:Label> </td>
                                            <td><%# Eval("couponName")%></td>
                                            <td><%# Eval("ItemCategory")%></td>
                                            <td><%# Convert.ToDateTime(Eval("validFrom")).ToString("MM/dd/yyyy H:mm:ss")%></td>
                                            <td><%# Convert.ToDateTime(Eval("validTo")).ToString("MM/dd/yyyy H:mm:ss")%></td>
                                            <td><asp:LinkButton runat="server" ID="editCoupon" OnClick="editCoupon_Click" CssClass="btn btn-primary"><i class="fa fa-pencil"></i> Edit</asp:LinkButton></td>
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
                            </asp:UpdatePanel>
</div>
</asp:Content>

