const puppeteer = require('puppeteer');

(async () => {
  const browser = await puppeteer.launch({
    headless: true, // Switch to true for headless mode
    args: [
      "--disable-gpu",
      "--remote-debugging-port=9222",
      "--no-sandbox",
      "--disable-setuid-sandbox",
    ],
  });

  // Test Case 1: Check for podcast table rows
  const page1 = await browser.newPage();
  try {
    await page1.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/podcastsList');
    await page1.waitForSelector('table tbody tr', { timeout: 5000 });

    const rowCount = await page1.$$eval('table tbody tr', rows => rows.length);

    if (rowCount > 0) {
      console.log('TESTCASE:Podcast_table_rows_exist:success');
    } else {
      console.log('TESTCASE:Podcast_table_rows_exist:failure');
    }
  } catch (e) {
    console.log('TESTCASE:Podcast_table_rows_exist:failure');
  }

  // Test Case 2: Form exists and has required fields
  const page2 = await browser.newPage();
  try {
    await page2.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addPodcast');
    const formExists = await page2.evaluate(() => {
      const form = document.querySelector('form');
      const inputFields = ['title', 'description', 'hostName', 'category', 'releaseDate', 'contactEmail', 'episodeCount'];
      return form && inputFields.every(field => !!form.querySelector(`[formControlName="${field}"]`));
    });

    if (formExists) {
      console.log('TESTCASE:Podcast_form_exists_and_has_required_fields:success');
    } else {
      console.log('TESTCASE:Podcast_form_exists_and_has_required_fields:failure');
    }
  } catch (e) {
    console.log('TESTCASE:Podcast_form_exists_and_has_required_fields:failure');
  }

  // Test Case 3: Check if submit button exists and has the correct name
  const page3 = await browser.newPage();
  try {
    await page3.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addPodcast');
    await page3.waitForSelector('form', { timeout: 5000 });

    const submitButton = await page3.$('button[type="submit"]');
    if (submitButton) {
      const buttonText = await page3.evaluate(button => button.textContent.trim(), submitButton);
      if (buttonText === 'Add Podcast') {
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

  // Test Case 4: Check if placeholders exist and are correct
  const page4 = await browser.newPage();
  try {
    await page4.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addPodcast');

    const placeholders = {
      title: 'Enter title',
      hostName: 'Enter host name',
      category: 'Enter category',
      releaseDate: 'Enter release date',
      contactEmail: 'Enter contact email',
      episodeCount: 'Enter episode count',
      description: 'Enter description'
    };

    const checkPlaceholders = async () => {
      const results = await Promise.all(Object.keys(placeholders).map(async field => {
        const input = await page4.$(`input[placeholder="${placeholders[field]}"], textarea[placeholder="${placeholders[field]}"]`);
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

  // Test Case 5: Check if input types are correct
  const page5 = await browser.newPage();
  try {
    await page5.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/addPodcast');

    const inputTypes = {
      'Enter title': 'text',
      'Enter host name': 'text',
      'Enter category': 'text',
      'Enter release date': 'date',
      'Enter contact email': 'email',
      'Enter episode count': 'number',
      'Enter description': 'textarea'
    };

    const checkInputTypes = async () => {
      const results = await Promise.all(Object.entries(inputTypes).map(async ([placeholder, expectedType]) => {
        const input = await page5.$(`input[placeholder="${placeholder}"], textarea[placeholder="${placeholder}"]`);
        if (input) {
          if (expectedType === 'textarea') {
            const isTextarea = await page5.evaluate(el => el.tagName.toLowerCase() === 'textarea', input);
            return isTextarea;
          } else {
            const inputType = await page5.evaluate(el => el.type, input);
            return inputType === expectedType;
          }
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

  // Test Case 6: Check if the table headers have correct text content
  const page6 = await browser.newPage();
  try {
    await page6.goto('https://8081-aabdbffdadabafcfd314190586ebabbcadeeefceacone.premiumproject.examly.io/podcastsList');

    const thTextContent = await page6.evaluate(() => {
      const expectedTexts = [
        'Title', 'Host Name', 'Category', 'Release Date', 'Episode Count', 'Action'
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

  // Close all pages and the browser
  await page1.close();
  await page2.close();
  await page3.close();
  await page4.close();
  await page5.close();
  await page6.close();
  await browser.close();

})();
