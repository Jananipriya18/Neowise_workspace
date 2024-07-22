// const puppeteer = require('puppeteer');
//     (async () => {
//     const browser = await puppeteer.launch({
//       headless: false,
//       args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox']
//     });
//     // Test case to verify the existence of correct heading, table, and back button in the booked batches page
//     const page = await browser.newPage();
//     try {
//       await page.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/Class/BookedClasses');
//       await page.setViewport({
//         width: 1200,
//         height: 1200,
//       });
  
//       await page.waitForSelector('h2', { timeout: 2000 });
//       await page.waitForSelector('#backButton', { timeout: 2000 });
//       await page.waitForSelector('table', { timeout: 2000 });

//       const rowCount = await page.$$eval('tr', rows => rows.length, { timeout: 2000 });
//       if (rowCount>1) 
//       {
//         console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_book_batches_page:success');
//       } 
//       else 
//       {
//       console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_book_batches_page:failure');
//       }
//     } catch (e) {
//       console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_book_batches_page:failure');
//     } 
//     // Test case to verify the existence of book and delete buttons in the available batches page
//     const page1 = await browser.newPage();
//     try {
//       await page1.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
//       await page1.setViewport({
//         width: 1200,
//         height: 1200,
//       });
//       await page1.waitForSelector('#deleteButton', { timeout: 2000 });
//       await page1.waitForSelector('#bookButton', { timeout: 2000 });
//       const rowCount = await page1.$$eval('tr', rows => rows.length, { timeout: 2000 });
    
//       if (rowCount >1) 
//       {
//         console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:success');
//       } 
//       else 
//       {
//         console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:failure');
//       }
//     } catch (e) {
//       console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_batches_page:failure');
//     }  
//     // Test case to verify the existence of back button and heading in the batch enrollment form page
//     const page2 = await browser.newPage();
//     try {
//       await page2.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
//       await page2.setViewport({
//         width: 1200,
//         height: 1200,
//       });
//       await page2.waitForSelector('#bookButton', { timeout: 2000 });
//       await page2.click('#bookButton');
//       const urlAfterClick = page2.url();
//       await page2.waitForSelector('#backtoclass', { timeout: 2000 });
//       const Message = await page2.$eval('h2', element => element.textContent.toLowerCase());
//     if(Message.includes("class enrollment form")&&urlAfterClick.toLowerCase().includes('booking/classenrollmentform'))
//     {
//     console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:success');
//     }    
//     else{
//     console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:failure');
//     }
//     } catch (e) {
//       console.log('TESTCASE:Existence_of_id_backtobatch_and_heading_in_batch_enrollment_form_page:failure');
//     } 


//     const page3 = await browser.newPage();
//     try {
//       await page3.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
//       await page3.setViewport({
//         width: 1200,
//         height: 1200,
//       });
//       await page3.waitForSelector('#deleteButton', { timeout: 2000 });
//       await page3.click('#deleteButton');
//       await page3.waitForSelector('h2', { timeout: 2000 });
//       const urlAfterClick = page3.url();
//          const Message = await page3.$eval('h2', element => element.textContent.toLowerCase());
//     if(Message.includes("delete class")&&urlAfterClick.toLowerCase().includes('class/deleteconfirmation'))
//     {
//     console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:success');
//     }    
//     else{
//     console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:failure');
//     }
//     } catch (e) {
//       console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:failure');
//     } 

//     finally{
//     await page.close();
//     await page1.close();
//     await page2.close();
//     await page3.close();
//     await browser.close();
//     }
  
// })();

const puppeteer = require('puppeteer');

(async () => {
    const browser = await puppeteer.launch({
        headless: false,
        args: ['--headless', '--disable-gpu', '--remote-debugging-port=9222', '--no-sandbox', '--disable-setuid-sandbox']
    });

    // Test case to verify the existence of correct heading, table, and back button in the booked tours page
    const page = await browser.newPage();
    try {
        await page.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/HistoricalTour/BookedTours');
        await page.setViewport({
            width: 1200,
            height: 1200,
        });

        await page.waitForSelector('h2', { timeout: 2000 });
        await page.waitForSelector('#backButton', { timeout: 2000 });
        await page.waitForSelector('table', { timeout: 2000 });

        const rowCount = await page.$$eval('tr', rows => rows.length, { timeout: 2000 });
        if (rowCount > 1) {
            console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_booked_tours_page:success');
        } else {
            console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_booked_tours_page:failure');
        }
    } catch (e) {
        console.log('TESTCASE:Existence_of_correct_heading_table_along_with_rows_and_back_button_in_booked_tours_page:failure');
    }

    // Test case to verify the existence of book and delete buttons in the available tours page
    const page1 = await browser.newPage();
    try {
        await page1.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
        await page1.setViewport({
            width: 1200,
            height: 1200,
        });
        await page1.waitForSelector('#deleteButton', { timeout: 2000 });
        await page1.waitForSelector('#bookButton', { timeout: 2000 });
        const rowCount = await page1.$$eval('tr', rows => rows.length, { timeout: 2000 });

        if (rowCount > 1) {
            console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_tours_page:success');
        } else {
            console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_tours_page:failure');
        }
    } catch (e) {
        console.log('TESTCASE:Existence_of_book_and_delete_button_and_table_along_with_rows_in_available_tours_page:failure');
    }

    // Test case to verify the existence of back button and heading in the tour enrollment form page
    const page2 = await browser.newPage();
    try {
        await page2.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
        await page2.setViewport({
            width: 1200,
            height: 1200,
        });
        await page2.waitForSelector('#bookButton', { timeout: 2000 });
        await page2.click('#bookButton');
        const urlAfterClick = page2.url();
        await page2.waitForSelector('#backtoTour', { timeout: 2000 });
        const message = await page2.$eval('h2', element => element.textContent.toLowerCase());
        if (message.includes("tour enrollment form") && urlAfterClick.toLowerCase().includes('booking/tourenrollmentform')) {
            console.log('TESTCASE:Existence_of_id_backtoTour_and_heading_in_tour_enrollment_form_page:success');
        } else {
            console.log('TESTCASE:Existence_of_id_backtoTour_and_heading_in_tour_enrollment_form_page:failure');
        }
    } catch (e) {
        console.log('TESTCASE:Existence_of_id_backtoTour_and_heading_in_tour_enrollment_form_page:failure');
    }

    // Test case to verify the existence of delete button and heading in the delete confirmation page
    const page3 = await browser.newPage();
    try {
        await page3.goto('https://8081-abfdabeabcbaed313801225dbafbddbadadbdone.premiumproject.examly.io/');
        await page3.setViewport({
            width: 1200,
            height: 1200,
        });
        await page3.waitForSelector('#deleteButton', { timeout: 2000 });
        await page3.click('#deleteButton');
        await page3.waitForSelector('h2', { timeout: 2000 });
        const urlAfterClick = page3.url();
        const message = await page3.$eval('h2', element => element.textContent.toLowerCase());
        if (message.includes("delete tour") && urlAfterClick.toLowerCase().includes('historicaltour/deleteconfirmation')) {
            console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:success');
        } else {
            console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:failure');
        }
    } catch (e) {
        console.log('TESTCASE:Existence_of_delete_button_and_heading_in_delete_confirmation_page:failure');
    }

    finally {
        await page.close();
        await page1.close();
        await page2.close();
        await page3.close();
        await browser.close();
    }

})();
