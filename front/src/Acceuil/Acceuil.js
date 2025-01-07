import React from 'react'
// import Header from './Header'
// import Footer from './Footer'
import ImageCarousel from './Carousel'
import TwoRowGrid from './Grid'
import FeaturesSection from './Service'

function Acceuil() {
  return (
    <div>
        <div style={{marginTop: "64px"}}>
          <ImageCarousel />
          <TwoRowGrid />
          
        </div>
        <FeaturesSection/>
    </div>
  )
}

export default Acceuil