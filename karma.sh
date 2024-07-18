#!/bin/bash
export CHROME_BIN=/usr/bin/chromium
if [ ! -d "/home/coder/project/workspace/angularapp" ]
then
    cp -r /home/coder/project/workspace/karma/angularapp /home/coder/project/workspace/;
fi

if [ -d "/home/coder/project/workspace/angularapp" ]
then
    echo "project folder present"
    cp /home/coder/project/workspace/karma/karma.conf.js /home/coder/project/workspace/angularapp/karma.conf.js;
    # checking for project.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/project.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/project.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/project.service.spec.ts;
    else
        echo "Frontend_should_be_create_project_service FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_errorcomponent FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_errorcomponent FAILED";
    fi

    # checking for manager-edit-project.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/manager-edit-project" ]
    then
        cp /home/coder/project/workspace/karma/manager-edit-project.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/manager-edit-project/manager-edit-project.component.spec.ts;
    else
        echo "Frontend_should_create_manager_edit_project_component FAILED";
        echo "Frontend_should_contain_edit_project_heading_in_the_manager_edit_project_component FAILED";
    fi

    # checking for analyst-view-project.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analyst-view-project" ]
    then
        cp /home/coder/project/workspace/karma/analyst-view-project.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analyst-view-project/analyst-view-project.component.spec.ts;
    else
        echo "Frontend_should_create_analyst_view_project_component FAILED";
        echo "Frontend_should_contain_projects_heading_in_the_analyst_view_project_component FAILED";
    fi

    # checking for analyst-view-requirement.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analyst-view-requirement" ]
    then
        cp /home/coder/project/workspace/karma/analyst-view-requirement.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analyst-view-requirement/analyst-view-requirement.component.spec.ts;
    else
        echo "Frontend_should_create_analyst_view_requirement_component FAILED";
        echo "Frontend_should_contain_view_requirements_heading_in_the_analyst_view_requirement_component FAILED";
    fi

    # checking for managerviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/managerviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/managerviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/managerviewfeedback/managerviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_manager_view_feedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_manager_view_feedback_component FAILED";
    fi

     # checking for manager-edit-project.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/manager-edit-project" ]
    then
        cp /home/coder/project/workspace/karma/manager-edit-project.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/manager-edit-project/manager-edit-project.component.spec.ts;
    else
        echo "Frontend_should_create_manager_view_project_component FAILED";
        echo "Frontend_should_contain_projects_heading_in_the_manager_view_project_component FAILED";
    fi

    # checking for feedback.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/feedback.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/feedback.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/feedback.service.spec.ts;
    else
        echo "Frontend_should_create_feedback_service FAILED";
    fi

    # checking for home.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/home" ]
    then
        cp /home/coder/project/workspace/karma/home.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/home/home.component.spec.ts;
    else
        echo "Frontend_should_create_home_component FAILED";
        echo "Frontend_should_contain_req_bridge_pro_heading_in_the_home_component FAILED";
    fi

    # checking for manager-view-requirement.component.spec.ts component
    if [ -d "/manager-view-requirement/coder/project/workspace/angularapp/src/app/components/manager-view-requirement" ]
    then
        cp /home/coder/project/workspace/karma/manager-view-requirement.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/manager-view-requirement/manager-view-requirement.component.spec.ts;
    else
        echo "Frontend_should_create_manager_view_requirement_component FAILED";
        echo "Frontend_should_contain_all_project_requirements_heading_in_the_manager_view_requirement_component FAILED";
    fi

    # checking for login.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/login" ]
    then
        cp /home/coder/project/workspace/karma/login.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/login/login.component.spec.ts;
    else
        echo "Frontend_should_create_login_component FAILED";
        echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    fi

    # checking for navbar.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/navbar" ]
    then
        cp /home/coder/project/workspace/karma/navbar.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/navbar/navbar.component.spec.ts;
    else
        echo "Frontend_should_create_navbar_component FAILED";
        echo "Frontend_should_contain_req_bridge_pro_heading_in_the_navbar_component FAILED";
    fi

    # checking for managernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/managernav" ]
    then
        cp /home/coder/project/workspace/karma/managernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/managernav/managernav.component.spec.ts;
    else
        echo "Frontend_should_create_manager_nav_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for analystaddfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analystaddfeedback" ]
    then
        cp /home/coder/project/workspace/karma/analystaddfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analystaddfeedback/analystaddfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_analystaddfeedback_component FAILED";
        echo "Frontend_should_contain_add_feedback_heading_in_the_analystaddfeedback_component FAILED";
    fi

    # checking for manager-add-project.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/manager-add-project" ]
    then
        cp /home/coder/project/workspace/karma/manager-add-project.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/manager-add-project/manager-add-project.component.spec.ts;
    else
        echo "Frontend_should_create_manager_add_project_component FAILED";
        echo "Frontend_should_contain_create_new_project_heading_in_the_manager_add_project_component FAILED";
    fi

    # checking for analystnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analystnav" ]
    then
        cp /home/coder/project/workspace/karma/analystnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analystnav/analystnav.component.spec.ts;
    else
        echo "Frontend_should_create_analystnav_component FAILED";
    fi

    # checking for analystviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analystviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/analystviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analystviewfeedback/analystviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_analyst_view_feedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_analyst_view_feedback_component FAILED";
    fi

    # checking for analyst-add-requirement.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/analyst-add-requirement" ]
    then
        cp /home/coder/project/workspace/karma/analyst-add-requirement.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/analyst-add-requirement/analyst-add-requirement.component.spec.ts;
    else
        echo "Frontend_should_create_analyst_add_requirement_component FAILED";
        echo "Frontend_should_contain_add_requirement_heading_in_the_analyst_add_requirement_component FAILED";
    fi

    if [ -d "/home/coder/project/workspace/angularapp/node_modules" ]; 
    then
        cd /home/coder/project/workspace/angularapp/
        npm test;
    else
        cd /home/coder/project/workspace/angularapp/
        yes | npm install
        npm test
    fi 
else   
    echo "Frontend_should_be_create_project_service FAILED";
    echo "Frontend_should_create_auth_service FAILED";
    echo "Frontend_should_create_errorcomponent FAILED";
    echo "Frontend_should_contain_wrong_message_in_the_errorcomponent FAILED";
    echo "Frontend_should_create_manager_edit_project_component FAILED";
    echo "Frontend_should_contain_edit_project_heading_in_the_manager_edit_project_component FAILED";
    echo "Frontend_should_create_analyst_view_project_component FAILED";
    echo "Frontend_should_contain_projects_heading_in_the_analyst_view_project_component FAILED";
    echo "Frontend_should_create_analyst_view_requirement_component FAILED";
    echo "Frontend_should_contain_view_requirements_heading_in_the_analyst_view_requirement_component FAILED";
    echo "Frontend_should_create_manager_view_feedback_component FAILED";
    echo "Frontend_should_contain_feedback_details_heading_in_the_manager_view_feedback_component FAILED";
    echo "Frontend_should_create_manager_view_project_component FAILED";
    echo "Frontend_should_contain_projects_heading_in_the_manager_view_project_component FAILED";
    echo "Frontend_should_create_feedback_service FAILED";
    echo "Frontend_should_create_home_component FAILED";
    echo "Frontend_should_contain_req_bridge_pro_heading_in_the_home_component FAILED";
    echo "Frontend_should_create_manager_view_requirement_component FAILED";
    echo "Frontend_should_contain_all_project_requirements_heading_in_the_manager_view_requirement_component FAILED";
    echo "Frontend_should_create_login_component FAILED";
    echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    echo "Frontend_should_create_navbar_component FAILED";
    echo "Frontend_should_contain_req_bridge_pro_heading_in_the_navbar_component FAILED";
    echo "Frontend_should_create_manager_nav_component FAILED";
    echo "Frontend_should_create_registration_component FAILED";
    echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    echo "Frontend_should_create_analystaddfeedback_component FAILED";
    echo "Frontend_should_contain_add_feedback_heading_in_the_analystaddfeedback_component FAILED";
    echo "Frontend_should_create_manager_add_project_component FAILED";
    echo "Frontend_should_contain_create_new_project_heading_in_the_manager_add_project_component FAILED";
    echo "Frontend_should_create_analystnav_component FAILED";
    echo "Frontend_should_create_analyst_view_feedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_analyst_view_feedback_component FAILED";
    echo "Frontend_should_create_analyst_add_requirement_component FAILED";
    echo "Frontend_should_contain_add_requirement_heading_in_the_analyst_add_requirement_component FAILED";
fi
