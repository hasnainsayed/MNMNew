<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="dispatchedDump.aspx.cs" Inherits="dispatchedDump" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper"> <section class="content-header">
        <h1>Dispatched Dump
        <!--<small>it all starts here</small>-->
        </h1>
        <ol class="breadcrumb">
            <li><a href="Default.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li>Reports</li>
            <li class="active">Dispatched Dump</li>
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
                        <h3 class="box-title">Dispatched Dump</h3>

                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">

                        <div class="form-group">
                            <asp:LinkButton ID="dispatchdownloadDump" runat="server" CssClass="btn btn-sm btn-primary" OnClick="dispatchdownloadDump_Click"><i class="fa fa-file-excel-o"></i> Download</asp:LinkButton>
                        </div>

                        </div> </div> </div> </div></section>
</ContentTemplate>
                <Triggers>
                                            <asp:PostBackTrigger ControlID="dispatchdownloadDump" />
                                            </Triggers>
               </asp:UpdatePanel>
        </div>
</asp:Content>

