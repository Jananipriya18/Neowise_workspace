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
    # checking for workout.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/workout.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/workout.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/workout.service.spec.ts;
    else
        echo "Frontend_should_create_workout_service FAILED";
    fi

    # checking for auth.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/auth.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/auth.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/auth.service.spec.ts;
    else
        echo "Frontend_should_create_auth_service FAILED";
    fi

    # checking for workoutrequest.service.spec.ts component
    if [ -e "/home/coder/project/workspace/angularapp/src/app/services/workoutrequest.service.ts" ]
    then
        cp /home/coder/project/workspace/karma/workoutrequest.service.spec.ts /home/coder/project/workspace/angularapp/src/app/services/workoutrequest.service.spec.ts;
    else
        echo "Frontend_should_create_workoutrequest_service FAILED";
    fi

    # checking for error.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/error" ]
    then
        cp /home/coder/project/workspace/karma/error.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/error/error.component.spec.ts;
    else
        echo "Frontend_should_create_error_component FAILED";
        echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    fi

    # checking for adminaddworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminaddworkout" ]
    then
        cp /home/coder/project/workspace/karma/adminaddworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminaddworkout/adminaddworkout.component.spec.ts;
    else
        echo "Frontend_should_create_adminaddworkout_component FAILED";
        echo "Frontend_should_contain_create_new_workout_heading_in_the_adminaddworkout_component FAILED";
    fi

    # checking for admineditworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/admineditworkout" ]
    then
        cp /home/coder/project/workspace/karma/admineditworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/admineditworkout/admineditworkout.component.spec.ts;
    else
        echo "Frontend_should_create_admineditworkout_component FAILED";
        echo "Frontend_should_contain_edit_workout_heading_in_the_admineditworkout_component FAILED";
    fi

    # checking for adminviewfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback" ]
    then
        cp /home/coder/project/workspace/karma/adminviewfeedback.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewfeedback/adminviewfeedback.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewfeedback_component FAILED";
        echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    fi

    # checking for adminviewworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminviewworkout" ]
    then
        cp /home/coder/project/workspace/karma/adminviewworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminviewworkout/adminviewworkout.component.spec.ts;
    else
        echo "Frontend_should_create_adminviewworkout_component FAILED";
        echo "Frontend_should_contain_workouts_heading_in_the_adminviewworkout_component FAILED";
    fi

     # checking for requestedworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/requestedworkout" ]
    then
        cp /home/coder/project/workspace/karma/requestedworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/requestedworkout/requestedworkout.component.spec.ts;
    else
        echo "Frontend_should_create_requestedworkout_component FAILED";
        echo "Frontend_should_contain_workout_requests_for_approval_heading_in_the_requestedworkout_component FAILED";
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
        echo "Frontend_should_contain_fitness_tracker_heading_in_the_home_component FAILED";
    fi

    # checking for useraddfeedback.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/useraddfeedback" ]
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
        echo "Frontend_should_contain_fitness_tracker_heading_in_the_navbar_component FAILED";
    fi

    # checking for adminnav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/adminnav" ]
    then
        cp /home/coder/project/workspace/karma/adminnav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/adminnav/adminnav.component.spec.ts;
    else
        echo "Frontend_should_create_adminnav_component FAILED";
    fi

    # checking for registration.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/registration" ]
    then
        cp /home/coder/project/workspace/karma/registration.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/registration/registration.component.spec.ts;
    else
        echo "Frontend_should_create_registration_component FAILED";
        echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    fi

    # checking for userappliedworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userappliedworkout" ]
    then
        cp /home/coder/project/workspace/karma/userappliedworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userappliedworkout/userappliedworkout.component.spec.ts;
    else
        echo "Frontend_should_create_userappliedworkout_component FAILED";
        echo "Frontend_should_contain_applied_workouts_heading_in_the_userappliedworkout_component FAILED";
    fi

    # checking for userviewworkout.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userviewworkout" ]
    then
        cp /home/coder/project/workspace/karma/userviewworkout.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userviewworkout/userviewworkout.component.spec.ts;
    else
        echo "Frontend_should_create_userviewworkout_component FAILED";
        echo "Frontend_should_contain_available_workouts_heading_in_the_userviewworkout_component FAILED";
    fi

    # checking for usernav.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/usernav" ]
    then
        cp /home/coder/project/workspace/karma/usernav.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/usernav/usernav.component.spec.ts;
    else
        echo "Frontend_should_create_usernav_component FAILED";
    fi

    # checking for userworkoutform.component.spec.ts component
    if [ -d "/home/coder/project/workspace/angularapp/src/app/components/userworkoutform" ]
    then
        cp /home/coder/project/workspace/karma/userworkoutform.component.spec.ts /home/coder/project/workspace/angularapp/src/app/components/userworkoutform/userworkoutform.component.spec.ts;
    else
        echo "Frontend_should_create_userworkoutform_component FAILED";
        echo "Frontend_should_contain_workout_application_form_heading_in_the_userworkoutform_component FAILED";
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
    echo "Frontend_should_create_workout_service FAILED";
    echo "Frontend_should_create_auth_service FAILED";
    echo "Frontend_should_create_college_service FAILED";
    echo "Frontend_should_create_error_component FAILED";
    echo "Frontend_should_contain_wrong_message_in_the_error_component FAILED";
    echo "Frontend_should_create_adminaddcollege_component FAILED";
    echo "Frontend_should_contain_create_new_college_heading_in_the_adminaddcollege_component FAILED";
    echo "Frontend_should_create_admineditcollege_component FAILED";
    echo "Frontend_should_contain_edit_college_heading_in_the_admineditcollege_component FAILED";
    echo "Frontend_should_create_adminviewcollege_component FAILED";
    echo "Frontend_should_contain_colleges_heading_in_the_adminviewcollege_component FAILED";
    echo "Frontend_should_create_adminviewfeedback_component FAILED";
    echo "Frontend_should_contain_feedback_details_heading_in_the_adminviewfeedback_component FAILED";
    echo "Frontend_should_create_requestedcollege_component FAILED";
    echo "Frontend_should_contain_college_application_requests_for_approval_heading_in_the_requestedcollege_component FAILED";
    echo "Frontend_should_create_feedback_service FAILED";
    echo "Frontend_should_create_home_component FAILED";
    echo "Frontend_should_contain_globaledconnect_heading_in_the_home_component FAILED";
    echo "Frontend_should_create_useraddfeedback_component FAILED";
    echo "Frontend_should_contain_add_feedback_heading_in_the_useraddfeedback_component FAILED";
    echo "Frontend_should_create_login_component FAILED";
    echo "Frontend_should_contain_login_heading_in_the_login_component FAILED";
    echo "Frontend_should_create_navbar_component FAILED";
    echo "Frontend_should_contain_globaledconnect_heading_in_the_navbar_component FAILED";
    echo "Frontent_should_create_adminnav_component FAILED";
    echo "Frontend_should_create_registration_component FAILED";
    echo "Frontend_should_contain_registration_heading_in_the_registration_component FAILED";
    echo "Frontend_should_create_userapplicationform_component FAILED";
    echo "Frontend_should_contain_college_application_form_heading_in_the_userapplicationform_component FAILED";
    echo "Frontend_should_create_userappliedcollege_component FAILED";
    echo "Frontend_should_contain_applied_colleges_heading_in_the_userappliedcollege_component FAILED";
    echo "Frontend_should_create_usernav_component FAILED";
    echo "Frontend_should_create_userviewcollege_component FAILED";
    echo "Frontend_should_contain_available_colleges_heading_in_the_userviewcollege_component FAILED";
    echo "Frontend_should_create_userviewfeedback_component FAILED";
    echo "Frontend_should_contain_my_feedback_heading_in_the_userviewfeedback_component FAILED";
fi
