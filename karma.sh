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
    # checking for orphanage.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/orphanage.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/orphanage.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/orphanage.service.spec.ts;
    else
        echo "Frontend_should_create_orphanage_service FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for donation.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/donation.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/donation.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/donation.service.spec.ts;
    else
        echo "Frontend_should_create_donation_service FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_errorcomponent FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_errorcomponent FAILED";
    fi

    # checking for useraddfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/useraddfeedback" ]
    then
        cp /home/coder/project/workspace/karma/useraddfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/useraddfeedback/useraddfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_useraddfeedback_component FAILED";
        echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    fi

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for viewalldonation.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/viewalldonation" ]
    then
        cp /home/coder/project/workspace/karma/viewalldonation.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/viewalldonation/viewalldonation.component.spec.ts;
    else
        echo "Frontend_should_create_viewalldonation_component FAILED";
        echo "Frontend_should_contain_all_donations_heading_in_the_viewalldonation_component FAILED";
    fi

    # checking for uservieworphanage.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/uservieworphanage" ]
    then
        cp /home/coder/project/workspace/karma/uservieworphanage.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/uservieworphanage/uservieworphanage.component.spec.ts;
    else
        echo "Frontend_should_create_uservieworphanage_component FAILED";
        echo "Frontend_should_contain_orphanages_heading_in_the_uservieworphanage_component FAILED";
    fi

    # checking for userviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/userviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewfeedback/userviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_userviewfeedback_component FAILED";
        echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
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
        echo "Frontend_should_contain_care_heaven_heading_in_the_home_component FAILED";
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
        echo "Frontend_should_contain_care_heaven_heading_in_the_navbar_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for request.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/request.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/request.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/request.service.spec.ts;
    else
        echo "Frontend_should_create_request_service FAILED";
    fi

    # checking for createorphanage.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/createorphanage" ]
    then
        cp /home/coder/project/workspace/karma/createorphanage.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/createorphanage/createorphanage.component.spec.ts;
    else
        echo "Frontend_should_create_createorphanage_component FAILED";
        echo "Frontend_should_contain_orphanage_creation_form_heading_in_the_createorphanage_component FAILED";
    fi

    # checking for adminnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminnav" ]
    then
        cp /home/coder/project/workspace/karma/adminnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminnav/adminnav.component.spec.ts;
    else
        echo "Frontend_should_create_adminnav_component FAILED";
    fi

    # checking for adminvieworphanage.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminvieworphanage" ]
    then
        cp /home/coder/project/workspace/karma/adminvieworphanage.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminvieworphanage/adminvieworphanage.component.spec.ts;
    else
        echo "Frontend_should_create_adminvieworphanage_component FAILED";
        echo "Frontend_should_contain_orphanages_heading_in_the_adminvieworphanage_component FAILED";
    fi

    # checking for adminviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/adminviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback/sellerviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewfeedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    fi

    # checking for mydonation.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/mydonation" ]
    then
        cp /home/coder/project/workspace/karma/mydonation.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/mydonation/mydonation.component.spec.ts;
    else
        echo "Frontend_should_create_mydonation_component FAILED";
        echo "Frontend_should_contain_donation_details_heading_in_the_mydonation_component FAILED";
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
    echo "Frontend_should_create_orphanage_service FAILED";
    echo "Frontend_should_create_auth_service FAILED";
    echo "Frontend_should_create_donation_service FAILED";
    echo "Frontend_should_create_errorcomponent FAILED";
    echo "Frontend_should_contain_wrong_message_in_the_errorcomponent FAILED";
    echo "Frontend_should_create_useraddfeedback_component FAILED";
    echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    echo "Frontend_should_create_usernav_component FAILED";
    echo "Frontend_should_create_viewalldonation_component FAILED";
    echo "Frontend_should_contain_all_donations_heading_in_the_viewalldonation_component FAILED";
    echo "Frontend_should_create_uservieworphanage_component FAILED";
    echo "Frontend_should_contain_orphanages_heading_in_the_uservieworphanage_component FAILED";
    echo "Frontend_should_create_userviewfeedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
    echo "Frontend_should_create_feedback_service FAILED";
    echo "Frontend_should_create_home_component FAILED";
    echo "Frontend_should_contain_care_heaven_heading_in_the_home_component FAILED";
    echo "Frontend_should_create_login_component FAILED";
    echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    echo "Frontend_should_create_navbar_component FAILED";
    echo "Frontend_should_contain_care_heaven_heading_in_the_navbar_component FAILED";
    echo "Frontend_should_create_registration_component FAILED";
    echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    echo "Frontend_should_create_createorphanage_component FAILED";
    echo "Frontend_should_contain_orphanage_creation_form_heading_in_the_createorphanage_component FAILED";
    echo "Frontend_should_create_adminnav_component FAILED";
    echo "Frontend_should_create_adminvieworphanage_component FAILED";
    echo "Frontend_should_contain_orphanages_heading_in_the_adminvieworphanage_component FAILED";
    echo "Frontend_should_create_adminviewfeedback_component FAILED";
    echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    echo "Frontend_should_create_mydonation_component FAILED";
    echo "Frontend_should_contain_donation_details_heading_in_the_mydonation_component FAILED";
fi
