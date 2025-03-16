
////alert("Hi");
//const cart = {};

//// Function to increase quantity
//function increaseQuantity(itemId) {
//    const quantityElement = document.getElementById(`quantity-${itemId}`);
//    let currentQuantity = parseInt(quantityElement.innerText);

//    // Increase the quantity by 1
//    currentQuantity += 1;
//    quantityElement.innerText = currentQuantity;
//}

//// Function to decrease quantity
//function decreaseQuantity(itemId) {
//    const quantityElement = document.getElementById(`quantity-${itemId}`);
//    let currentQuantity = parseInt(quantityElement.innerText);

//    // Decrease the quantity by 1, but not below zero
//    if (currentQuantity > 0) {
//        currentQuantity -= 1;
//        quantityElement.innerText = currentQuantity;
//    }
//}

//// Function to add item to cart
//function addToCart(itemId) {
//    const quantityElement = document.getElementById(`quantity-${itemId}`);
//    const quantity = parseInt(quantityElement.innerText);

//    if (quantity > 0) {
//        // Add item to the cart or update its quantity
//        if (cart[itemId]) {
//            cart[itemId] += quantity;  // Increment existing item quantity
//        } else {
//            cart[itemId] = quantity;  // Add new item with its quantity
//        }

//        // Reset the displayed quantity after adding to cart
//        quantityElement.innerText = 0;

//        // Update the cart display
//        updateCartDisplay();

//        // Update the cart view with the current items
//        updateCartView();
//    } else {
//        alert("Please select a quantity before adding to cart.");
//    }
//}

//// Function to update the cart display with the current total quantity
//function updateCartDisplay() {
//    const totalQuantity = Object.values(cart).reduce((sum, qty) => sum + qty, 0);
//    const cartDisplayElement = document.getElementById('cart-count');
//    if (cartDisplayElement) {
//        cartDisplayElement.innerText = `Cart (${totalQuantity})`;
//    }
//}

//// Function to update the cart view with the list of items in the cart
//function updateCartView() {
//    const cartViewElement = document.getElementById('cart-view');
//    cartViewElement.innerHTML = '';  // Clear previous items

//    // Display each item in the cart with its current quantity
//    for (const [itemId, quantity] of Object.entries(cart)) {
//        const cartItem = document.createElement('div');
//        cartItem.className = 'cart-item';
//    }

//    // Show a message if the cart is empty
//    if (Object.keys(cart).length === 0) {
//        cartViewElement.innerHTML = '<p>No items in cart</p>';
//    }
//}

function redirectToCart() {
    window.location.href = "/Cart/Cart";
}

// Declare cart as a regular variable so it can be updated dynamically
let cart = {};

// Function to load the cart from localStorage
function loadCartFromStorage() {
    const storedCart = localStorage.getItem('cart');
    if (storedCart) {
        Object.assign(cart, JSON.parse(storedCart)); // Load cart data
    }
}

// Function to save the cart to localStorage
function saveCartToStorage() {
    localStorage.setItem('cart', JSON.stringify(cart));
}

// Function to increase quantity
function increaseQuantity(itemId) {
    const quantityElement = document.getElementById(`quantity-${itemId}`);
    let currentQuantity = parseInt(quantityElement.innerText);

    // Increase the quantity by 1
    currentQuantity += 1;
    quantityElement.innerText = currentQuantity;
}

// Function to decrease quantity
function decreaseQuantity(itemId) {
    const quantityElement = document.getElementById(`quantity-${itemId}`);
    let currentQuantity = parseInt(quantityElement.innerText);

    // Decrease the quantity by 1, but not below zero
    if (currentQuantity > 0) {
        currentQuantity -= 1;
        quantityElement.innerText = currentQuantity;
    }
}

// Function to add item to cart
function addToCart(itemId) {
    const quantityElement = document.getElementById(`quantity-${itemId}`);
    const quantity = parseInt(quantityElement.innerText);

    if (quantity > 0) {
        // Add item to the cart or update its quantity
        if (cart[itemId]) {
            cart[itemId] += quantity; // Increment existing item quantity
        } else {
            cart[itemId] = quantity; // Add new item with its quantity
        }

        // Save the updated cart to localStorage
        saveCartToStorage();

        // Reset the displayed quantity after adding to cart
        quantityElement.innerText = 0;

        // Update the cart display and view
        updateCartDisplay();
        //updateCartView();
    } else {
        alert("Please select a quantity before adding to cart.");
    }
}

// Function to remove an item from the cart
function removeCartItem(itemId) {
    delete cart[itemId]; // Remove the item from the cart
    saveCartToStorage(); // Save the updated cart to localStorage
    updateCartView(); // Refresh the cart view
    updateCartDisplay(); // Refresh the cart display
}

// Function to update the cart display with the current total quantity
function updateCartDisplay() {
    const totalQuantity = Object.values(cart).reduce((sum, qty) => sum + qty, 0);
    const cartDisplayElement = document.getElementById('cart-count');
    if (cartDisplayElement) {
        cartDisplayElement.innerText = `Cart (${totalQuantity})`;
    }
}

// Function to update the cart view with the list of items in the cart
//function updateCartView() {
//    //const cartViewElement = document.getElementById('cart-view');
//    //cartViewElement.innerHTML = ''; // Clear previous items

//    // Display each item in the cart with its current quantity
//    for (const [itemId, quantity] of Object.entries(cart)) {
//        const itemData = getItemData(itemId); // Retrieve item data
//        const cartItem = document.createElement('div');
//        cartItem.className = 'cart-item';
//        cartItem.innerHTML = `
//            <strong>${itemData.name}</strong> - Quantity: ${quantity} 
//            <span>Price: $${(itemData.price * quantity).toFixed(2)}</span>
//            <button onclick="removeCartItem('${itemId}')">Remove</button>
//        `;
//        cartViewElement.appendChild(cartItem);
//    }

//    // Show a message if the cart is empty
//    if (Object.keys(cart).length === 0) {
//        cartViewElement.innerHTML = '<p>No items in cart</p>';
//    }
//}

// Function to retrieve item data by ID (assuming you have item info stored)
function getItemData(itemId) {
    const items = {
        "1": { name: "Organic Apples", price: 0.99 },
        "2": { name: "Whole Wheat Bread", price: 2.49 },
        "3": { name: "Free-Range Eggs", price: 3.99 },
        "4": { name: "Almond Milk", price: 2.99 },
        "5": { name: "Broccoli", price: 1.29 },
        "6": { name: "Chicken Breast", price: 5.99 },
        "7": { name: "Organic Brown Rice", price: 4.49 },
        "8": { name: "Greek Yogurt", price: 1.49 },
        "9": { name: "Honey", price: 6.99 },
        "10": { name: "Carrots", price: 0.89 }
    };
    return items[itemId];
}

// Redirect to cart page
//function redirectToCart() {
//    window.location.href = "/Cart/MyOrders";
//}

// Call loadCartFromStorage when the page loads to sync cart with localStorage
document.addEventListener('DOMContentLoaded', function () {
    loadCartFromStorage();
    //updateCartView();
    updateCartDisplay();
});
