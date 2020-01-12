<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Vozaci.aspx.cs" Inherits="PPPKProjekt.Vozaci" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vozaci</h2>
    <div class="container bg-light mt-3 p-2">
        <div class="row">
            <div class="col-6">
                <fieldset>
                    <legend>Pregled vozaca</legend>

                    <asp:DropDownList ID="ddlDrivers" runat="server" AutoPostBack="True"
                        Width="150px" OnSelectedIndexChanged="ddlDrivers_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />

                    <div class="row">
                        <div class="col-6">
                            <span>Ime:</span>
                            <asp:Label ID="lbName" runat="server" />
                        </div>
                        <div class="col-6">
                            <span>Prezime:</span>
                            <asp:Label ID="lbLastname" runat="server" />
                        </div>
                    </div>

                </fieldset>
            </div>
            <div class="col-6"></div>
        </div>
        <asp:Label ID="lblInfo" runat="server" Font-Bold="True" Font-Size="11px"
            ForeColor="Red"></asp:Label>
    </div>
</asp:Content>
