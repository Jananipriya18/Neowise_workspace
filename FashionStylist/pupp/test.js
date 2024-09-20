const puppeteer = require('puppeteer');
(async () => {
  const browser = await puppeteer.launch({
    headless: false,
    args: [
      "--headless",
      "--disable-gpu",
      "--remote-debugging-port=9222",
      "--no-sandbox",
      "--disable-setuid-sandbox",
    ],
  });
        
    // Test Case 1: Check for input placeholder.
    const page1 = await browser.newPage();
    try {
      await page1.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/stylistsList'); // Replace with your actual test page URL
      await page1.waitForSelector('table tbody tr', { timeout: 5000 });
      
      const rowCount = await page1.$$eval('table tbody tr', rows => rows.length);
  
      if (rowCount > 0) {
        console.log('TESTCASE:Stylist_table_rows_exist:success');
      } else {
        console.log('TESTCASE:Stylist_table_rows_exist:failure');
      }
    } catch (e) {
      console.log('TESTCASE:Stylist_table_rows_exist:failure');
    } 

    const page2 = await browser.newPage();
    try {
      await page2.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addStylist'); // Replace with your actual test page URL
      const formExists = await page2.evaluate(() => {
        const form = document.querySelector('form');
        const inputFields = ['name', 'expertise', 'styleSignature', 'availability', 'hourlyRate', 'location'];
        const formHasInputFields = inputFields.every(field => !!form.querySelector(`[formControlName="${field}"]`));
        return !!form && formHasInputFields;
      });
  
      if (formExists) {
        console.log('TESTCASE:Stylist_form_exists_and_has_required_fields:success');
      } else {
        console.log('TESTCASE:Stylist_form_exists_and_has_required_fields:failure');
      }
    } catch (e) {
      console.log('TESTCASE:Stylist_form_exists_and_has_required_fields:failure');
    } 
  
   
    const page3 = await browser.newPage();
    try {
      await page3.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addStylist'); // Replace with your actual test page URL
      await page3.waitForSelector('form', { timeout: 5000 });

    const submitButton = await page3.$('button[type="submit"]');
    if (submitButton) {
      const buttonText = await page3.evaluate(button => button.textContent.trim(), submitButton);
      if (buttonText === 'Add Stylist') {
        console.log('TESTCASE:Submit_button_exists_and_has_correct_name:success');
      } else {
        console.log('TESTCASE:Submit_button_exists_and_has_correct_name:failure');
      }
    } else {
      console.log('TESTCASE:Submit_button_exists_and_has_correct_name:failure');
    }
  } catch (e) {
    console.log('TESTCASE:Submit_button_exists_and_has_correct_name:failure');
  } 


  const page4 = await browser.newPage();
  try {
    await page4.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addStylist'); // Replace with your actual test page URL

    const placeholders = {
      name: 'Enter Name',
      expertise: 'Enter expertise',
      styleSignature: 'Enter style signature',
      availability: 'Enter availability',
      hourlyRate: 'Enter hourly rate',
      location: 'Enter location'
    };

    const checkPlaceholders = async () => {
      const results = await Promise.all(Object.keys(placeholders).map(async field => {
        const input = await page4.$(`input[placeholder="${placeholders[field]}"]`);
        if (input) {
          const placeholder = await page4.evaluate(el => el.placeholder, input);
          return placeholder === placeholders[field];
        }
        return false;
      }));
      return results.every(result => result);
    };

    if (await checkPlaceholders()) {
      console.log('TESTCASE:Input_placeholders_exist_and_correct:success');
    } else {
      console.log('TESTCASE:Input_placeholders_exist_and_correct:failure');
    }
    } catch (e) {
      console.log('TESTCASE:Input_placeholders_exist_and_correct:failure');
    } 

    const page5 = await browser.newPage();
    try {
    await page5.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addStylist'); // Replace with your actual test page URL
    // Define expected types for each input
    const inputTypes = {
      'Enter Name': 'text',
      'Enter expertise': 'text',
      'Enter style signature': 'text',
      'Enter availability': 'text',
      'Enter Support Contact (Email)': 'email',
    };

    const checkInputTypes = async () => {
      const results = await Promise.all(Object.entries(inputTypes).map(async ([placeholder, expectedType]) => {
        const input = await page5.$(`input[placeholder="${placeholder}"]`);
        if (input) {
          const inputType = await page5.evaluate(el => el.type, input);
          return inputType === expectedType;
        }
        return false;
      }));
      return results.every(result => result);
    };
    if (await checkInputTypes()) {
      console.log('TESTCASE:Input_types_are_correct:success');
    } else {
      console.log('TESTCASE:Input_types_are_correct:failure');
    }
  } catch (e) {
    console.log('TESTCASE:Input_types_are_correct:failure');
  } 


  const page6 = await browser.newPage();
  try {
    await page6.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/stylistsList'); // Replace with your actual test page URL

    // Check if the th elements with expected text content exist
    const thTextContent = await page6.evaluate(() => {
      const expectedTexts = [
        'Title',
        'Release Year',
        'Genre',
        'Developer',
        'Support Contact',
        'Action'
      ];
      const thElements = document.querySelectorAll('table thead th');
      const thTexts = Array.from(thElements).map(th => th.textContent.trim());
      return expectedTexts.every(text => thTexts.includes(text));
    });

    if (thTextContent) {
      console.log('TESTCASE:TH_elements_with_text_content_exist:success');
    } else {
      console.log('TESTCASE:TH_elements_with_text_content_exist:failure');
    }
  } catch (e) {
    console.log('TESTCASE:TH_elements_with_text_content_exist:failure');
  } 
  

finally {
  await page1.close();
  await page2.close();
  await page3.close();
  await page4.close();
  await page5.close();
  await page6.close();
  await browser.close();
  }
})();

