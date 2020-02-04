<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="salesNdispatchdump.aspx.cs" Inherits="salesNdispatchdump" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="content-wrapper"> <section class="content-header">
        <h1>Stock And Dispatch Dump
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Stock And Dispatch Dump</li>
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
                        <h3 class="box-title">Stock And Dispatch Dump</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <div class="form-group">
                            <asp:LinkButton ID="salesndispatchdump" runat="server" CssClass="btn btn-sm btn-primary" OnClick="salesndispatchdump_Click"><i class="fa fa-file-excel-o"></i> Download</asp:LinkButton>
                        </div>

                        </div> </div> </div> </div></section>
</ContentTemplate>
                <Triggers>
                                            <asp:PostBackTrigger ControlID="salesndispatchdump" />
                                            </Triggers>
               </asp:UpdatePanel>
        </div>
</asp:Content>

