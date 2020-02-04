<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addCoupon.aspx.cs" Inherits="addCoupon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Add Coupon</h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="couponListing.aspx">Coupon</a></li>
                <li class="active">Add Coupon</li>
            </ol>
        </section>
        <section class="content">
            <!-- Default box -->
            <div class="box">
                <div class="box-body">
                    <!-- BOx -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">Add Coupon</h3>

                        </div>
                        <!-- /.box-header -->
                        <div class="box-body table-responsive">
                            <div class="row">
                                <asp:Label ID="Label1" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                <div id="divError" runat="server" visible="false" class="alert alert-danger alert-dismissible" style="padding-bottom: 0px; padding-top: 0px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
                                    <h5><i class="icon fa fa-check"></i>Saved Succesfully</h5>

                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>


                                        <asp:Label ID="couponId" runat="server" Text="" Visible="false"></asp:Label>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Coupon Name </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="couponName"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Coupon Name is required." ForeColor="Red" /><br />
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
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Valid From is required." ForeColor="Red" /><br />
                                                <asp:TextBox ID="validFrom" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Valid To </label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="validTo"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Valid To is required." ForeColor="Red" /><br />
                                                <asp:TextBox ID="validTo" runat="server" CssClass="form-control form_datetime"></asp:TextBox>
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
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Percentage/Amount is required." ForeColor="Red" />
                                                <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                                    ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                    ControlToValidate="couponDiscount" ValidationGroup="grp" ForeColor="Red" />
                                                <asp:TextBox ID="couponDiscount" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Applicable On Amount </label>
                                                <asp:DropDownList runat="server" ID="applicableOnAmount" CssClass="form-control select2">
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Amount </label>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
                                                    ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
                                                    ControlToValidate="applicableAmount" ValidationGroup="grp" ForeColor="Red" />
                                                <asp:TextBox ID="applicableAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-12">
                                                <asp:Button ID="dropdownAdd" runat="server" Text="Add New" class="btn bg-purple btn-round margin btn-sm" OnClick="dropdownAdd_Click" />
                                            </div>
                                            <table class="table table-bordered table-striped">
                                                <thead>
                                                    <tr>
                                                        <th>DropDown Values</th>
                                                        <th>Values</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rpt_dropdown" runat="server" OnItemDataBound="rpt_dropdown_ItemDataBound">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label runat="server" ID="TableName" Text='<%# Eval("TableName").ToString() %>' Visible="false"></asp:Label>
                                                                    <asp:DropDownList ID="drp_dropdown" CssClass="form-control select2" runat="server" DataTextField="SettingName" DataValueField="CTSettingID" AutoPostBack="true" OnSelectedIndexChanged="drp_dropdown_SelectedIndexChanged"></asp:DropDownList>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drp_dropValues" CssClass="form-control select2" runat="server" DataTextField="valueName" DataValueField="valueId"></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="box-body table-responsive ">
                                                <div class="box-footer btn-toolbar " style="margin-left: 0px;">
                                                    <asp:Button ID="cancel" runat="server" Text="Cancel" CssClass="btn btn-sm btn-danger pull-right" OnClick="cancel_Click" OnClientClick="return confirm('Do you Want to Cancel ?');" />
                                                    <asp:Button ID="save" runat="server" Text="Save" CssClass="btn btn-sm btn-success pull-right" OnClick="save_Click" ValidationGroup="grp" OnClientClick="return confirm('Do you Want to Save Coupon Settings ?');" />



                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="save" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

