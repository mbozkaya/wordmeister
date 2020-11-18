import React from "react";
import tw from "twin.macro";
import { css } from "styled-components/macro"; //eslint-disable-line
import AnimationRevealPage from "../../helpers/AnimationRevealPage.js";
import Hero from "../../components/hero/TwoColumnWithInput.js";
import Features from "../../components/features/ThreeColWithSideImage.js";
import MainFeature from "../../components/features/TwoColWithButton.js";
import MainFeature2 from "../../components/features/TwoColWithTwoHorizontalFeaturesAndButton.js";
import FeatureWithSteps from "../../components/features/TwoColWithSteps.js";
import Pricing from "../../components/pricing/ThreePlans.js";
import Testimonial from "../../components/testimonials/TwoColumnWithImageAndRating.js";
import FAQ from "../../components/faqs/SingleCol.js";
import GetStarted from "../../components/cta/GetStarted";
import Footer from "../../components/footers/MiniCenteredFooter.js";
import heroScreenshotImageSrc from "../../images/hero-screenshot-1.png";
import macHeroScreenshotImageSrc from "../../images/hero-screenshot-2.png";
import prototypeIllustrationImageSrc from "../../images/prototype-illustration.svg";
import { ReactComponent as BriefcaseIcon } from "feather-icons/dist/icons/briefcase.svg";
import { ReactComponent as MoneyIcon } from "feather-icons/dist/icons/dollar-sign.svg";
import { AuthContext } from "../../contexts/authContext.js";

const Home = () => {
  const Subheading = tw.span`uppercase tracking-widest font-bold text-primary-500`;
  const HighlightedText = tw.span`text-primary-500`;

  return (
    <AuthContext.Consumer>
      {({ authorize, onLogout }) => {
        return (
          <AnimationRevealPage>
            <Hero roundedHeaderButton={true} authorize={authorize} onLogout={onLogout} />
            <Features
              subheading={<Subheading>Features</Subheading>}
              heading={
                <>
                  We have Amazing <HighlightedText>Service.</HighlightedText>
                </>
              }
            />
            <FAQ
              subheading={<Subheading>FAQS</Subheading>}
              heading={
                <>
                  You have <HighlightedText>Questions ?</HighlightedText>
                </>
              }
              faqs={[
                {
                  question: "Are all the templates easily customizable ?",
                  answer:
                    "Yes, they all are. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },
                {
                  question: "How long do you usually support an standalone template for ?",
                  answer:
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },
                {
                  question: "What kind of payment methods do you accept ?",
                  answer:
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },
                {
                  question: "Is there a subscribption service to get the latest templates ?",
                  answer:
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },
                {
                  question: "Are the templates compatible with the React ?",
                  answer:
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                },
                {
                  question: "Do you really support Internet Explorer 11 ?",
                  answer:
                    "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat."
                }
              ]}
            />
            <GetStarted />
            <Footer />
          </AnimationRevealPage>

        )
      }
      }
    </AuthContext.Consumer>
  );
}

export default Home;