// Sample product data
const products = [
    {
        id: 1,
        name: 'Classic White T-Shirt',
        price: 29.99,
        category: 'men',
        image: 'https://placehold.co/300x400',
        description: 'Premium cotton crew neck t-shirt',
        sizes: ['S', 'M', 'L', 'XL'],
        colors: ['white', 'black', 'gray']
    },
    // Add more products as needed
];

// Cart functionality
let cart = [];

// DOM Elements
const productGrid = document.getElementById('productGrid');
const cartModal = document.getElementById('cartModal');
const productModal = document.getElementById('productModal');
const cartCount = document.getElementById('cartCount');
const cartItems = document.getElementById('cartItems');
const cartTotal = document.getElementById('cartTotal');
const categoryFilter = document.getElementById('categoryFilter');
const searchInput = document.getElementById('searchInput');
const navToggle = document.getElementById('navToggle');
const navLinks = document.getElementById('navLinks');

// Event Listeners
document.addEventListener('DOMContentLoaded', () => {
    displayProducts(products);
    loadCart();
});

navToggle.addEventListener('click', () => {
    navLinks.classList.toggle('active');
});

categoryFilter.addEventListener('change', filterProducts);
searchInput.addEventListener('input', filterProducts);

document.getElementById('cartBtn').addEventListener('click', toggleCart);
document.getElementById('closeCart').addEventListener('click', toggleCart);
document.getElementById('closeProduct').addEventListener('click', toggleProductModal);

document.getElementById('newsletterForm').addEventListener('submit', (e) => {
    e.preventDefault();
    alert('Thank you for subscribing!');
    e.target.reset();
});

// Product Display Functions
function displayProducts(productsToShow) {
    productGrid.innerHTML = productsToShow.map(product => `
        <div class="product-card" onclick="showProductDetails(${product.id})">
            <img src="${product.image}" alt="${product.name}">
            <div class="product-info">
                <h3>${product.name}</h3>
                <p class="price">$${product.price.toFixed(2)}</p>
                <button class="btn" onclick="event.stopPropagation(); addToCart(${product.id})">
                    Add to Cart
                </button>
            </div>
        </div>
    `).join('');
}

function filterProducts() {
    const category = categoryFilter.value;
    const searchTerm = searchInput.value.toLowerCase();

    const filteredProducts = products.filter(product => {
        const matchCategory