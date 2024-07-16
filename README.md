# Assign Permissions
This small demo repository show how to handle the new SharePoint granular list(item) permissions for Microsoft Graph

## Summary

The repository consists of three parts: AssigPermissionsBeta realizes the assignment of permissions using Microsoft Graph Beta. 
TestPermissions tests the granted permissions. 
PSScript contains a PowerShell script to do the same.

For further details see the author's [blog post](https://mmsharepoint.wordpress.com/2024/07/16/new-granular-permission-model-in-sharepoint/)

## Version history

Version|Date|Author|Comments
-------|----|--------|--------
1.0|July 16, 2024|[Markus Moeller](http://www.twitter.com/moeller2_0)|Initial release


## Disclaimer

**THIS CODE IS PROVIDED _AS IS_ WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.**

## Minimal Path to Awesome

- Clone this repository
    ```bash
    git clone https://github.com/mmsharepoint/AssignPermissions.git
    ```
- You will need to register TWO apps in Entra ID 
  - Platform Desktop and Redirect http://localhost
  - with **delegated** Graph permissions:
    - App 1 Sites.FullControl.All
    - App 2 Lists.SelectedOperations.Selected, ListItems.SelectedOperations.Selected
- Copy AssignPermissionsBeta\appsettings.sample.json to AssignPermissionsBeta\appsettings.json and fill in your data, use app 1
- Copy TestPermissions\appsettings.sample.json to TestPermissions\appsettings.json and fill in your data, use app 2
- In AssignPermissionsBeta press F5 to debug and assign permissions
- In TestPermissions press F5 to debug and test permissions
- Alternatively, fill in your data into PSScript\AssignPermissions.ps1 and test here, potentially stey-by-step (F8)

## Features

This small demo illustrates the following concepts:

- [Overview of Selected permissions in OneDrive and SharePoint](https://learn.microsoft.com/en-us/graph/permissions-selected-overview?view=graph-rest-1.0&tabs=http&WT.mc_id=M365-MVP-5004617)



