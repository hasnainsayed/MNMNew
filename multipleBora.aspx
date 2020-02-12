<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="multipleBora.aspx.cs" Inherits="multipleBora" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    


                
     
        
<div class="content-wrapper">
        <section class="content-header">
            <h1>Multiple Bora
        <!--<small>it all starts here</small>-->
            </h1>
            <ol class="breadcrumb">
                <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
                <li><a href="newLot.aspx">Bora</a></li>
                <li class="active">Multiple Bora</li>
            </ol>
        </section>
        <section class="content">
            <!-- /.box-header -->
            <div class="row">
                <div class="col-xs-12">


                    <div class="box">

                        <div class="box-body table-responsive">
                            <div class="row">

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vendor </label>
                                    <asp:DropDownList ID="vendorID" runat="server" CssClass="form-control select2" DataTextField="VendorName" DataValueField="VendorID"></asp:DropDownList>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>No. of Bora </label>
                                    <asp:TextBox runat="server" ID="noOfBora" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total No. of Pieces/Bora </label>    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="totalPiece"
                                                    ValidationGroup="grp" runat="server" ErrorMessage="Is required." ForeColor="Red" /><br />
                                  <asp:TextBox ID="totalPiece" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total Amount/Bora </label> 
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

                             
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>LR </label>
                                    <asp:DropDownList ID="lrno" runat="server" CssClass="form-control select2" DataTextField="lrno" DataValueField="id"></asp:DropDownList>
                                </div>

                            </div>

                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>Travel Cost/Bora </label>
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
            <!-- /.box -->
                </div>
                <!-- /.col -->
            </div>
                </div>
        </section>
    </div>
</asp:Content>