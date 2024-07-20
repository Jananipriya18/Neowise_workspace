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
    # checking for application.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/application.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/application.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/application.service.spec.ts;
    else
        echo "Frontend_should_create_application_service FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for college.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/college.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/college.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/college.service.spec.ts;
    else
        echo "Frontend_should_create_college_service FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_error_component FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    fi

    # checking for adminaddcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminaddcollege" ]
    then
        cp /home/coder/project/workspace/karma/adminaddcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminaddcollege/adminaddcollege.component.spec.ts;
    else
        echo "Frontend_should_create_adminaddcollege_component FAILED";
        echo "Frontend_should_contain_create_new_college_heading_in_the_adminaddcollege_component FAILED";
    fi

    # checking for admineditcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/admineditcollege" ]
    then
        cp /home/coder/project/workspace/karma/admineditcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/admineditcollege/admineditcollege.component.spec.ts;
    else
        echo "Frontend_should_create_admineditcollege_component FAILED";
        echo "Frontend_should_contain_edit_college_heading_in_the_admineditcollege_component FAILED";
    fi

    # checking for adminviewcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewcollege" ]
    then
        cp /home/coder/project/workspace/karma/adminviewcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewcollege/adminviewcollege.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewcollege_component FAILED";
        echo "Frontend_should_contain_colleges_heading_in_the_adminviewcollege_component FAILED";
    fi

    # checking for adminviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/adminviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback/managerviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewfeedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    fi

     # checking for requestedcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/requestedcollege" ]
    then
        cp /home/coder/project/workspace/karma/requestedcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/requestedcollege/requestedcollege.component.spec.ts;
    else
        echo "Frontend_should_create_requestedcollege_component FAILED";
        echo "Frontend_should_contain_college_application_requests_for_approval_heading_in_the_requestedcollege_component FAILED";
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
        echo "Frontend_should_contain_globaledconnect_heading_in_the_home_component FAILED";
    fi

    # checking for useraddfeedback.component.spec.ts component
    if [ -d "/useraddfeedback/coder/project/workspace/angularapp/src/app/components/useraddfeedback" ]
    then
        cp /home/coder/project/workspace/karma/useraddfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/useraddfeedback/useraddfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_useraddfeedback_component FAILED";
        echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
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
        echo "Frontend_should_contain_globaledconnect_heading_in_the_navbar_component FAILED";
    fi

    # checking for adminnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminnav" ]
    then
        cp /home/coder/project/workspace/karma/adminnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminnav/adminnav.component.spec.ts;
    else
        echo "Frontent_should_create_adminnav_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for userapplicationform.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userapplicationform" ]
    then
        cp /home/coder/project/workspace/karma/userapplicationform.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userapplicationform/userapplicationform.component.spec.ts;
    else
        echo "Frontend_should_create_userapplicationform_component FAILED";
        echo "Frontend_should_contain_college_application_form_heading_in_the_userapplicationform_component FAILED";
    fi

    # checking for userappliedcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userappliedcollege" ]
    then
        cp /home/coder/project/workspace/karma/userappliedcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userappliedcollege/userappliedcollege.component.spec.ts;
    else
        echo "Frontend_should_create_userappliedcollege_component FAILED";
        echo "Frontend_should_contain_applied_colleges_heading_in_the_userappliedcollege_component FAILED";
    fi

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for userviewcollege.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewcollege" ]
    then
        cp /home/coder/project/workspace/karma/userviewcollege.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewcollege/userviewcollege.component.spec.ts;
    else
        echo "Frontend_should_create_userviewcollege_component FAILED";
        echo "Frontend_should_contain_available_colleges_heading_in_the_userviewcollege_component FAILED";
    fi

    # checking for userviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/userviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewfeedback/userviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_userviewfeedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
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
